using System;
using System.Collections.Generic;
using System.IO;

namespace AMC2GIFT
{
    class AMCTXT
    {
        internal static Boolean analyzeFile(string path)
        {
            /*
			 * Methode pour lire le fichier config en chemin dans la variable source Si une
			 * ligne du fichier correspond à un parametre, changer la valeur du parametre
			 * avec celle dans le fichier (si valeur valide) Gere aussi les questions dans
			 * le fichier source et les mets dans une liste de questions.
			 */
            Boolean error = false;
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
                                error =analyzeQuestion(ligne, file, nbligne, error);
                            }
                            else // si c'est pas une *, alors c'est un parametre
                            {
                                int n = ligne.IndexOf(":"); // recherche de position du premier ":" pour pouvoir separer le nom du param de
                                                            // sa valeur
                                if (n == -1)
                                {
                                    Console.WriteLine("Erreur de syntaxe à la ligne " + nbligne);
                                    error = true;
                                }// si -1 alors il n'y a pas de : et donc ce n'est pas un paramètre							}
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
                Console.WriteLine(e.Message);
                error = true;
            }
            return error;
        }

        private static Boolean analyzeQuestion(string ligne, StreamReader file, int nbligne, Boolean error)
        {
            ligne = file.ReadLine();
            while (!ligne.Equals(""))
            {
                if (!ligne.Substring(0, 1).Equals("+") && !ligne.Substring(0, 1).Equals("-"))
                {
                    if (ligne.Substring(0, 1).Equals("*"))
                    {
                        Console.WriteLine("Erreur de syntaxe à la ligne " + nbligne);
                        error = true;
                    }
                }
                ligne = file.ReadLine();
                nbligne++;
            }
            return error;
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
                        String[] spl = options.Split("=");
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
                Console.WriteLine(e.Message);
            }
            return gps;
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

        internal static void exportQuestions(List<Groupe> gquestions, string destpath, int verbose)
        {
            using (StreamWriter w = new StreamWriter(destpath, false))
            {
                String res = "";
                int nbquestions = 0;
                int nbquestionsignore = 0;
                for (int i = 0; i < gquestions.Count; i++)
                {
                    if (i != 0)
                    {
                        res += "*(";
                        res += gquestions[i].getTexte()[0] + "\n";
                    }
                    foreach (Question q in gquestions[i].getQuestions())
                    {
                        nbquestions++;
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
                        else nbquestionsignore++;
                    }
                    if (i != 0)
                    {
                        res += "*)";
                        res += gquestions[i].getTexte()[1] + "\n";
                    }
                }
                if (nbquestionsignore > 0)
                    Console.WriteLine((nbquestions - nbquestionsignore) + " questions sur " + nbquestions + " converties");
                else Console.WriteLine(nbquestions + " questions converties");
                w.Write(res);
            }
        }
    }

}
