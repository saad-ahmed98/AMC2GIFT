using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AMC2GIFT
{
    class GIFT
    {
        internal static void analyzeFile(string path)
        {
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
                            Console.WriteLine("Sytax error at line " + nbligne + ", answers not closed");
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
                                    analyseReponses(sb.ToString(), nbligne);
                                    aOpen = false;
                                }
                            }
                        }
                    }
                    if (aOpen)
                        Console.WriteLine("Syntax error at end of file, an answer block is not closed.");
                    nbligne++;
                    i++;
                }

                file.Close();
                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void analyseReponses(String sb, int nbligne)
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
                Console.WriteLine("Syntax error at line " + nbligne + ". Illegal syntax in answers block");
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
