using System;
using System.Collections.Generic;
using System.IO;

namespace AMC2GIFT
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
                                    Console.WriteLine("Syntax error at line " + nbligne);
                                }// si -1 alors il n'y a pas de : et donc ce n'est pas un paramètre							}
                            }
                        }
                    }
                    ligne = file.ReadLine();
                    nbligne++;
                }
                file.Close();
                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
						Console.WriteLine("Syntax error at line " + nbligne);
				}
				ligne = file.ReadLine();
				nbligne++;
			}
		}

		internal static List<Groupe> importQuestions(string sourcepath, int verbose)
        {
            throw new NotImplementedException();
        }

        internal static void exportQuestions(string destpath, int verbose)
        {
            throw new NotImplementedException();
        }
    }
}
