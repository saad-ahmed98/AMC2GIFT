using System;
using System.Collections.Generic;
using System.IO;

namespace AMC2GIFT_GUI
{
    class AMC
    {
        internal static void analyzeFile(string path)
        {
            /*
			 * Methode pour lire le fichier config en chemin dans la variable source Si une
			 * ligne du fichier correspond à un parametre, changer la valeur du parametre
			 * avec celle dans le fichier (si valeur valide) Gere aussi les questions dans
			 * le fichier source et les mets dans une liste de questions.
			 */
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                String ligne;
                int nbligne = 0;
                ligne = file.ReadLine();
                nbligne++;
                // ligne pour gerer le code FEFF en UTF-8 BOM qui peut apparaitre si le fichier
                // txt est edité avec windows notepad
                // ce caractere apparait uniquement en debut de fichier
                if (ligne.Substring(0, 1).Equals("\uFEFF"))
                {
                    ligne = ligne.Substring(1);
                }

                while (ligne != null)
                {
                    if (!ligne.Equals(""))
                    {
                        if (!(ligne.Substring(0, 1).Equals("#")) && !(ligne.Substring(0, 1).Equals("\\"))) // si ligne commence par un # c'est un commentaire, donc
                                                                                                           // on
                                                                                                           // l'ignore
                        {
                            if (ligne.Substring(0, 1).Equals("*")) // si ligne commence par une *, c'est une question
                            {
                                analyzeQuestion(ligne, file, nbligne);
                            }
                            else // si c'est pas une *, alors c'est un parametre
                            {
                                int n = ligne.IndexOf(":"); // recherche de position du premier ":" pour pouvoir separer le nom du param de
                                                            // sa valeur
                                if (n == -1)
                                {
                                    Analyze.logbox.AppendText("Erreur de syntaxe à la ligne " + nbligne);
                                    Analyze.logbox.AppendText(Environment.NewLine);

                                }// si -1 alors il n'y a pas de : et donc ce n'est pas un paramètre	
                            }
                        }
                    }
                    ligne = file.ReadLine();
                    nbligne++;
                }
                file.Close();
            }
            catch (Exception e)
            {
                Analyze.logbox.AppendText(e.Message);
            }
        }

        private static void analyzeQuestion(string ligne, StreamReader file, int nbligne)
        {
            ligne = file.ReadLine();
            while (!ligne.Equals(""))
            {
                if (!ligne.Substring(0, 1).Equals("+") && !ligne.Substring(0, 1).Equals("-"))
                {
                    if (ligne.Substring(0, 1).Equals("*"))
                    {
                        Analyze.logbox.AppendText("Erreur de syntaxe à la ligne " + nbligne);
                        Analyze.logbox.AppendText(Environment.NewLine);
                    }
                }
                ligne = file.ReadLine();
                nbligne++;
            }
        }

        private static Object makeQuestion(Groupe gp, Boolean open, string ligne)
        {
            String type = ligne.Substring(0, 2);
            Question q = null;
            Groupe g = null;
            switch (type)
            {
                case "*<":
                    q = new QuestionOuverteAMC();
                    int nblignes = 1;
                    int debutl = ligne.IndexOf("<");
                    int finl = ligne.IndexOf(">");
                    if ((debutl != -1) && (finl != -1))
                    {
                        String options = ligne.Substring(debutl + 1, finl - 2);
                        String[] spl = options.Split('=');
                        if (spl[0].ToLower().Equals("lines"))
                        {
                            nblignes = int.Parse(spl[1]);
                        }
                        q.addTitre(ligne.Substring(finl + 1));
                    }
                    ((QuestionOuverte)q).setLignes(nblignes);
                    break;
                case "**":
                    q = new QuestionMultiple();
                    break;
                case "*(":
                    g = new Groupe();
                    g.addTexte1(ligne.Substring(3));
                    break;
                case "*)":
                    if (open)
                    {

                        open = false;
                        if (ligne.Length > 2)
                        {
                            gp.addTexte2(ligne.Substring(2));
                        }
                    }
                    break;
                default:
                    q = new QuestionSimple();
                    break;
            }
            int debutopt = ligne.IndexOf("{");
            int finopt = ligne.IndexOf("}");
            if ((debutopt != -1) && (finopt != -1))
            {
                ligne = ligne.Substring(finopt + 2);
            }
            else ligne = ligne.Substring(2);
            if (q != null)
            {
                if (q.titre.Count == 0)
                    q.addTitre(ligne);
                return q;
            }
            return g;
        }
        internal static void logExport(Question q)
        {
            Conversion.logbox.AppendText(Environment.NewLine);
            Conversion.logbox.AppendText("Exportation de question de type " + q.GetType().Name + "...");
            Conversion.logbox.AppendText(Environment.NewLine);
            Conversion.logbox.AppendText("Titre question : " + q.getTitre());
            Conversion.logbox.AppendText(Environment.NewLine);
            Conversion.logbox.AppendText("Nombre de reponses : " + q.getReponses().Count);
            Conversion.logbox.AppendText(Environment.NewLine);
        }
        internal static void logImport(Question q)
        {
            Conversion.logbox.AppendText(Environment.NewLine);
            Conversion.logbox.AppendText("Lecture de question de type " + q.GetType().Name + "...");
            Conversion.logbox.AppendText(Environment.NewLine);
            Conversion.logbox.AppendText("Titre question : " + q.getTitre());
            Conversion.logbox.AppendText(Environment.NewLine);
            Conversion.logbox.AppendText("Nombre de reponses : " + q.getReponses().Count);
            Conversion.logbox.AppendText(Environment.NewLine);
        }
        internal static List<Groupe> importQuestions(string sourcepath, int verbose)
        {
            List<Groupe> gps = new List<Groupe>();
            gps.Add(new Groupe());
            Boolean open = false;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(sourcepath);
                String ligne;
                int nbligne = 0;
                Object res;
                ligne = file.ReadLine();
                nbligne++;
                // ligne pour gerer le code FEFF en UTF-8 BOM qui peut apparaitre si le fichier
                // txt est edité avec windows notepad
                // ce caractere apparait uniquement en debut de fichier
                if (ligne.Substring(0, 1).Equals("\uFEFF"))
                {
                    ligne = ligne.Substring(1);
                }

                while (ligne != null)
                {
                    if (!ligne.Equals(""))
                    {
                        if (!(ligne.Substring(0, 1).Equals("#")) && !(ligne.Substring(0, 1).Equals("\\"))) // si ligne commence par un # c'est un commentaire, donc
                                                                                                           // on
                                                                                                           // l'ignore
                        {
                            if (ligne.Substring(0, 1).Equals("*")) // si ligne commence par une *, c'est une question
                            {
                                res = makeQuestion(gps[gps.Count - 1], open, ligne);
                                nbligne++;
                                if (res is Question)
                                {
                                    Boolean end = false;
                                    ligne = file.ReadLine();
                                    nbligne++;
                                    while (!ligne.Equals("") && !end)
                                    {
                                        if (!ligne.Substring(0, 1).Equals("+") && !ligne.Substring(0, 1).Equals("-"))
                                        {
                                            if (ligne.Substring(0, 2).Equals("*)"))
                                            {
                                                res = makeQuestion(gps[gps.Count - 1], open, ligne);
                                                end = true;
                                            }
                                            else
                                            {
                                                ((Question)res).addTitre(ligne);
                                            }
                                        }
                                        else
                                        {
                                            switch (ligne.Substring(0, 1))
                                            {
                                                case "+":
                                                    ((Question)res).addReponse(new ReponseSimple("", ligne.Substring(2), true));
                                                    break;
                                                case "-":
                                                    ((Question)res).addReponse(new ReponseSimple("", ligne.Substring(2), false));
                                                    break;
                                            }
                                        }
                                        ligne = file.ReadLine();
                                        nbligne++;
                                    }
                                    gps[gps.Count - 1].addQuestion((Question)res);
                                    logImport((Question)res);
                                }
                                if (res is Groupe)
                                {
                                    if (res != null && !open)
                                    {
                                        gps.Add((Groupe)res);
                                        open = true;
                                    }
                                }
                            }
                        }
                    }
                    ligne = file.ReadLine();
                    nbligne++;
                }
                file.Close();
            }
            catch (Exception e)
            {
                Conversion.logbox.AppendText(e.Message);
            }
            return gps;
        }

        internal static void exportQuestions(List<Groupe> gquestions, string destpath, int verbose)
        {
            destpath += @"\resultatConversion.txt";
            using (StreamWriter w = new StreamWriter(destpath, false))
            {
                String res = "";
                for (int i = 0; i < gquestions.Count; i++)
                {
                    if (i != 0)
                    {
                        res += "*(";
                        res += gquestions[i].getTexte()[0] + "\n";
                    }
                    foreach (Question q in gquestions[i].getQuestions())
                    {
                        logExport(q);
                        String typeq = "";
                        if (q is QuestionSimple)
                        {
                            typeq = "* ";
                        }
                        if (q is QuestionMultiple)
                        {
                            typeq = "** ";
                        }
                        if (q is QuestionOuverte)
                        {
                            typeq = "*<lines=" + ((QuestionOuverte)q).getLignes() + "> ";
                            QuestionOuverte qo = (QuestionOuverte)q;
                            if (!(q is QuestionOuverteAMC))
                                qo.getReponses().Clear();
                            if (qo.getReponses().Count < 2)
                            {
                                qo.getReponses().Clear();
                                qo.addReponse(new ReponseSimple("", "Ok", true));
                                qo.addReponse(new ReponseSimple("", "Erreur", false));
                            }
                        }
                        if (typeq != "")
                        {
                            res += typeq + q.getTitre() + "\n";
                            foreach (Reponse r in q.getReponses())
                            {
                                ReponseSimple rs = (ReponseSimple)r;
                                String vf = "";
                                if (rs.vrai)
                                    vf = "+ ";
                                else vf = "- ";
                                res += vf + rs.texte + "\n";
                            }
                            res += "\n";
                        }
                    }
                    if (i != 0)
                    {
                        res += "*)";
                        res += gquestions[i].getTexte()[1] + "\n";
                    }
                }
                w.Write(res);
            }
        }
    }

}
