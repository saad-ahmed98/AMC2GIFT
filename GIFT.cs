using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AMC2GIFT
{
    class GIFT
    {
        internal static Boolean analyzeFile(string path)
        {
            Boolean error = false;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                String allFile;
                int nbligne = 0;
                Boolean aOpen = false;
                allFile = file.ReadToEnd();

                // ligne pour gerer le code FEFF en UTF-8 BOM qui peut apparaitre si le fichier
                // txt est edité avec windows notepad
                // ce caractere apparait uniquement en debut de fichier
                if (allFile.Substring(0, 1).Equals("\uFEFF"))
                {
                    allFile = allFile.Substring(1);
                }
                string[] lines = allFile.Split('\r');
                int i = 0;
                while (i < lines.Length)
                {

                    if (lines[i].Length == 1)
                    {
                        if (aOpen)
                        {
                            Console.WriteLine("Erreur de syntaxe à la ligne " + nbligne + ", bloc de question non fermé");
                            error = true;
                        }
                        aOpen = false;
                    }
                    else
                    {
                        if (!(lines[i].Substring(0, 2).Equals("//"))) // si ligne commence par un # c'est un commentaire, donc on l'ignore
                        {
                            for (int j = 0; j < lines[i].Length; j++)
                            {
                                if (lines[i][j] == '\\') j++;
                                if (lines[i][j] == '{')
                                {
                                    aOpen = true;
                                    StringBuilder sb = new StringBuilder();
                                    j++;
                                    if (j == lines[i].Length)
                                    {
                                        j = 0;
                                        i++;
                                        nbligne++;
                                    } while (lines[i][j] != '}' && i < lines.Length)
                                    {
                                        if (j == lines[i].Length)
                                        {
                                            j = 0;
                                            i++;
                                            nbligne++;
                                        }
                                        else
                                        {
                                            sb.Append(lines[i][j]);
                                            j++;
                                            if (j == lines[i].Length)
                                            {
                                                j = 0;
                                                i++;
                                                nbligne++;
                                            }
                                        }
                                    }
                                    error = analyseReponses(sb.ToString(), nbligne, error);
                                    aOpen = false;
                                }
                            }
                        }
                    }
                    if (aOpen)
                    {
                        Console.WriteLine("Erreur de syntaxe à la fin du fichier. Un bloc de question n'est pas fermé.");
                        error = true;
                    }
                    nbligne++;
                    i++;
                }

                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                error = true;
            }
            return error;
        }

        internal static void logExport(Question q)
        {
            /*Console.WriteLine("Exportation de question de type " + q.GetType().Name + "...");
            Console.WriteLine("Titre question : " + q.getTitre());
            Console.WriteLine("Nombre de reponses : " + q.getReponses().Count);
            */
        }
        internal static void logImport(Question q)
        {
            /*
            Console.WriteLine("Lecture de question de type " + q.GetType().Name + "...");
            Console.WriteLine("Titre question : " + q.getTitre());
            Console.WriteLine("Nombre de reponses : " + q.getReponses().Count);
            */
        }

        private static Boolean analyseReponses(String sb, int nbligne, Boolean error)
        {
            Boolean match = true;
            if (sb.Length > 0)
            {
                match = false;
                Boolean numerical = false;
                Regex rx3 = new Regex(@"^(FALSE|TRUE|T|F)(#([A-z0-9]*[^A-Za-z0-9#{}~=]*)*){0,2}$");
                Regex rx4 = new Regex(@"^((~|=)*([A-z0-9]*[^A-Za-z0-9#{}~=]*)*(#[A-z0-9]*[^A-Za-z0-9#{}~=]*)*\n*)*$");

                if (rx3.IsMatch(sb))
                    match = true;
                else if (sb.Substring(0, 1).Equals("#")) numerical = true;
                if (!match)
                {
                    if (numerical)
                    {
                        Regex rx1 = new Regex(@"^#([0-9]+\.?[0-9]*)(\:)?([0-9]+\.?[0-9]*)?$");
                        Regex rx2 = new Regex(@"^#([0-9]+\.?[0-9]*)(\.\.)?([0-9]+\.?[0-9]*)?$");
                        if (rx1.IsMatch(sb)) match = true;
                        else if (rx2.IsMatch(sb)) match = true;
                        else if (rx4.IsMatch(sb)) match = true;
                    }
                    else if (rx4.IsMatch(sb)) match = true;
                }
            }
            if (!match)
            {
                Console.WriteLine("Erreur de syntaxe à la ligne " + nbligne + ". Syntaxe illegale dans un bloc de question");
                error = true;
            }
            return error;
        }

        internal static List<Groupe> importQuestions(string sourcepath, int verbose)
        {
            List<Groupe> res = new List<Groupe>();
            Groupe g = new Groupe();
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(sourcepath);
                String allFile;
                int nbligne = 0;
                allFile = file.ReadToEnd();
                String titreq = "";
                // ligne pour gerer le code FEFF en UTF-8 BOM qui peut apparaitre si le fichier
                // txt est edité avec windows notepad
                // ce caractere apparait uniquement en debut de fichier
                if (allFile.Substring(0, 1).Equals("\uFEFF"))
                {
                    allFile = allFile.Substring(1);
                }
                string[] lines = allFile.Split('\r');
                int i = 0;
                while (i < lines.Length)
                {

                    if (lines[i].Length == 1)
                    {
                        //Console.WriteLine(lines[i]);
                    }
                    else
                    {
                        if (!(lines[i].Substring(0, 2).Equals("//")) && !(lines[i].Substring(0, 3).Equals("\n//"))) // si ligne commence par un # c'est un commentaire, donc on l'ignore
                        {
                            for (int j = 0; j < lines[i].Length; j++)
                            {

                                if (lines[i][j].Equals('\\')) j++;
                                else if (lines[i][j] == '{')
                                {
                                    StringBuilder sb = new StringBuilder();
                                    j++;
                                    if (j == lines[i].Length)
                                    {
                                        j = 0;
                                        i++;
                                        nbligne++;
                                    } while (lines[i][j] != '}' && i < lines.Length)
                                    {
                                        if (j == lines[i].Length)
                                        {
                                            j = 0;
                                            i++;
                                            nbligne++;
                                        }
                                        else
                                        {
                                            sb.Append(lines[i][j]);
                                            j++;
                                            if (j == lines[i].Length)
                                            {
                                                j = 0;
                                                i++;
                                                nbligne++;
                                            }
                                        }
                                    }
                                    Question q = makeQuestion(sb.ToString(), titreq);
                                    g.addQuestion(q);
                                    logImport(q);
                                    titreq = "";
                                }
                                else titreq += lines[i][j];
                            }
                        }
                    }
                    nbligne++;
                    i++;
                }

                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            res.Add(g);
            return res;
        }

        private static Question makeQuestion(string reponses, string titreq)
        {
            Question res = null;
            String titrequestion = getTitreQuestion(titreq);
            String nomquestion = getNomQuestion(titreq);
            String typequestion = getTypeQuestion(reponses);
            titrequestion = titrequestion.Replace("\n", "");
            nomquestion = nomquestion.Replace("\n", "");

            List<Reponse> reps = getReponses(reponses);
            switch (typequestion)
            {
                case "TRUEFALSE":
                    res = new QuestionVF();
                    break;
                case "ESSAY":
                    res = new QuestionEssay();
                    break;
                case "NUMERICAL":
                    res = new QuestionNumerical();
                    break;
                case "SIMPLE":
                    res = new QuestionSimple();
                    break;
                case "MULTIPLE":
                    res = new QuestionMultiple();
                    break;
                case "SHORTANSWER":
                    res = new QuestionShort();
                    break;
                case "APPARIEMENT":
                    res = new QuestionAppariement();
                    break;
            }
            foreach (Reponse r in reps)
            {
                cleanReponse(r);
                res.addReponse(r);
            }
            res.addTitre(titrequestion);
            res.nomq = nomquestion;
            return res;
        }

        private static void cleanReponse(Reponse r)
        {
            if (r is ReponseSimple)
            {
                ReponseSimple rm = (ReponseSimple)r;
                String texte = rm.texte;
                texte = Regex.Replace(texte, @"%-?[0-9]+(\.[0-9]+)?%", "");
                texte = texte.Replace("\n", "");
                texte = Regex.Replace(texte, @"####.*", "");
                int index = texte.IndexOf("#");
                if (index != -1)
                {
                    String[] split = texte.Split("#");
                    texte = split[0];
                    rm.feedbackR = split[1];
                }
                rm.texte = texte;
            }
            if (r is ReponseMatching)
            {
                ReponseMatching rm = (ReponseMatching)r;
                String texte1 = rm.texte1;
                texte1 = texte1.Replace("\n", "");
                String texte2 = rm.texte2;
                texte2 = texte2.Replace("\n", "");

                texte2 = Regex.Replace(texte2, @"####.*", "");
                int index = texte2.IndexOf("#");
                if (index != -1)
                {
                    String[] split = texte2.Split("#");
                    texte2 = split[0];
                    rm.feedbackR = split[1];
                }
                rm.texte1 = texte1;
                rm.texte2 = texte2;
            }
        }

        private static List<Reponse> getReponses(string reponses)
        {
            Regex rx3 = new Regex(@"^(FALSE|TRUE|T|F)(#([A-z0-9]*[^A-Za-z0-9#{}~=]*)*){0,2}$");
            List<Reponse> res = new List<Reponse>();
            if (rx3.IsMatch(reponses.ToUpper()))
            {
                switch (reponses.Substring(0, 1).ToUpper())
                {
                    case "T":
                        res.Add(new ReponseSimple("", "TRUE", true));
                        res.Add(new ReponseSimple("", "FALSE", false));
                        break;
                    case "F":
                        res.Add(new ReponseSimple("", "TRUE", false));
                        res.Add(new ReponseSimple("", "FALSE", true));
                        break;
                }
            }
            else if (reponses.IndexOf("=") == -1 && reponses.IndexOf("~") == -1)
            {
                if (reponses.Substring(0, 1).Equals("#"))
                    reponses = reponses.Substring(1);
                res.Add(new ReponseSimple("", reponses, true));
            }
            else if (reponses.IndexOf("=") != -1 && reponses.IndexOf("~") == -1)
            {
                String[] split = reponses.Split("=");
                if (reponses.IndexOf("->") != -1)
                {
                    for (int i = 1; i < split.Length; i++)
                    {
                        String[] texteappariement = Regex.Split(split[i], "->");
                        if (texteappariement.Length == 2)
                        {
                            res.Add(new ReponseMatching(texteappariement[0], texteappariement[1]));
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < split.Length; i++)
                    {
                        res.Add(new ReponseSimple("", split[i], true));
                    }
                }
            }
            else if (reponses.IndexOf("=") == -1 && reponses.IndexOf("~") != -1)
            {
                String[] split = reponses.Split("~");
                for (int i = 1; i < split.Length; i++)
                {
                    String fraction = "";
                    Boolean vrai = false;
                    String entitule = split[i];
                    int index1 = split[i].IndexOf('%');
                    int index2 = split[i].IndexOf('%', split[i].IndexOf('%') + 1);
                    if (index1 != -1 && index2 != -1)
                    {
                        fraction = split[i].Substring(index1 + 1, index2 - 1);
                        if (Double.Parse(fraction) > 0) vrai = true;
                        entitule = split[i].Substring(index2 + 1);
                    }
                    ReponseSimple r = new ReponseSimple("", entitule, vrai);
                    r.fraction = fraction;
                    res.Add(r);
                }
            }
            else
            {
                Regex rx1 = new Regex(@"(~|=)[^~=]*");
                MatchCollection ms = rx1.Matches(reponses);
                foreach (Match m in ms)
                {
                    Boolean vrai = false;
                    String entitule = "";
                    if (m.Value.Substring(0, 1).Equals("=")) vrai = true;
                    entitule = m.Value.Substring(1);
                    res.Add(new ReponseSimple("", entitule, vrai));
                }
            }
            return res;
        }

        private static string getTypeQuestion(string reponses)
        {
            Regex rx3 = new Regex(@"^(FALSE|TRUE|T|F)(#([A-z0-9]*[^A-Za-z0-9#{}~=]*)*){0,2}$");

            if (rx3.IsMatch(reponses.ToUpper()))
                return "TRUEFALSE";
            if (reponses.IndexOf("=") == -1 && reponses.IndexOf("~") == -1)
            {
                if (reponses.Substring(0, 1).Equals("#"))
                    return "NUMERICAL";
                return "ESSAY";
            }
            if (reponses.IndexOf("=") != -1 && reponses.IndexOf("~") == -1)
            {
                if (reponses.IndexOf("->") != -1)
                    return "APPARIEMENT";
                return "SHORTANSWER";
            }
            if (reponses.IndexOf("=") == -1 && reponses.IndexOf("~") != -1)
                return "MULTIPLE";
            return "SIMPLE";
        }

        private static String getNomQuestion(string titreq)
        {
            int start = titreq.IndexOf("::");
            Boolean found1 = false;
            Boolean found2 = false;
            if (start != -1)
            {
                titreq = titreq.Substring(start + 2);
                int i = 0;
                while (!found2 && i < titreq.Length)
                {
                    if (!found1 && titreq[i].Equals(':'))
                        found1 = true;
                    else if (found1 && titreq[i].Equals(':'))
                        found2 = true;
                    i++;
                }
                if (found1 && found2)
                {
                    return titreq.Substring(0, i - 2);
                }
            }
            return "";
        }

        private static String getTitreQuestion(string titreq)
        {
            int start = titreq.IndexOf("::");
            Boolean found1 = false;
            Boolean found2 = false;
            if (start != -1)
            {
                titreq = titreq.Substring(start + 2);
                int i = 0;
                while (!found2 && i < titreq.Length)
                {
                    if (!found1 && titreq[i].Equals(':'))
                        found1 = true;
                    else if (found1 && titreq[i].Equals(':'))
                        found2 = true;
                    i++;
                }
                if (found1 && found2)
                {
                    return titreq.Substring(i);
                }
            }
            return titreq;
        }

        internal static void exportQuestions(List<Groupe> gquestions, string destpath, int verbose)
        {
            StreamWriter w = new StreamWriter(destpath, false);
            String res = "";
            int nbquestions = 0;
            foreach (Groupe g in gquestions)
            {
                int i = 1;
                foreach (Question q in g.getQuestions())
                {
                    logExport(q);
                    if (q.nomq.Equals(""))
                        q.nomq = "Question " + i;
                    if (q.getTitre().Equals(""))
                        q.titre[0] = "No title";
                    res += "::" + q.nomq + "::";
                    res += q.getTitre();
                    nbquestions++;
                    switch (q)
                    {
                        case QuestionDescription qd:
                            res += "\n\n";
                            break;
                        case QuestionVF qvf:
                            res += "{" + qvf.getTrueOrFalse() + "}\n\n";
                            break;
                        case QuestionSimple qs:
                            res += exportReponsesSimple((Question)qs);
                            break;
                        case QuestionMultiple qm:
                            res += exportReponsesMultiple(qm);
                            break;
                        case QuestionAppariement qmt:
                            res += exportReponsesAppariement(qmt);
                            break;
                        case QuestionNumerical qn:
                            res += "{#" + ((ReponseSimple)qn.getReponses()[0]).texte + "}\n\n";
                            break;
                        case QuestionShort qs:
                            res += exportReponsesSimple((Question)qs);
                            break;
                        default:
                            res += "{}\n\n";
                            break;
                    }
                    i++;
                }
            }
            Console.WriteLine(nbquestions + " questions converties");
            w.Write(res);
            w.Close();
        }

        private static string exportReponsesAppariement(QuestionAppariement qmt)
        {
            String res = "{\n";
            foreach (Reponse r in qmt.getReponses())
            {
                ReponseMatching rm = (ReponseMatching)r;
                res += "=" + rm.texte1 + " => " + rm.texte2;
                res += "\n";
            }
            if (!qmt.feedbackQ.Equals(""))
            {
                res += "####" + qmt.feedbackQ + "\n";
            }
            res += "}\n\n";
            return res;
        }

        private static string exportReponsesSimple(Question q)
        {
            String res = "{\n";
            foreach (Reponse r in q.getReponses())
            {
                ReponseSimple rs = (ReponseSimple)r;
                String type = "";
                if (rs.isTrue()) type = "=";
                else type = "~";
                res += type + rs.texte;
                if (!rs.feedbackR.Equals(""))
                    res += "#" + rs.feedbackR;
                res += "\n";
            }
            if (!q.feedbackQ.Equals(""))
            {
                res += "####" + q.feedbackQ + "\n";
            }
            res += "}\n\n";
            return res;
        }

        private static string exportReponsesMultiple(QuestionMultiple q)
        {
            String res = "{\n";
            foreach (Reponse r in q.getReponses())
            {
                ReponseSimple rs = (ReponseSimple)r;
                String fraction = "";
                if (rs.isTrue()) fraction = "100";
                else fraction = "-100";
                res += "~%" + fraction + "%" + rs.texte;
                if (!rs.feedbackR.Equals(""))
                    res += "#" + rs.feedbackR;
                res += "\n";
            }
            if (!q.feedbackQ.Equals(""))
            {
                res += "####" + q.feedbackQ + "\n";
            }
            res += "}\n\n";
            return res;
        }
    }
}
