using Microsoft.VisualBasic.CompilerServices;
using System;


namespace AMC2GIFT
{
    class Controller
    {
        static int verbose = 1;
        static void Main(string[] args)
        {
            if (args.Length < 1)
                Console.WriteLine("Aucun argument, utiliser -h ou --help pour ouvrir le manuel.");
            else switch (args[0])
                {
                    case "-c":
                        convertFile(args);
                        break;
                    case "--convert":
                        convertFile(args);
                        break;
                    case "-a":
                        analyzeFile(args);
                        break;
                    case "-h":
                        showHelp();
                        break;
                    case "--help":
                        showHelp();
                        break;
                    case "--analyze":
                        analyzeFile(args);
                        break;
                    default:
                        Console.WriteLine("Arguments illegaux, utiliser -h ou --help pour ouvrir le manuel.");
                        break;
                }
        }

        private static void convertFile(string[] args)
        {
            int i = 1;
            Boolean stop = false;
            if (args.Length < 5)
            {
                Console.WriteLine("Arguments illegaux pour --convert, pour convertir utiliser '-c [-v <verboselevel>] <sourceformat> <sourcepath> <destformat> <destpath>'");
                stop = true;
            }
            else if (args[i].Equals("-v") || args[i].Equals("--verbose"))
            {
                if (args.Length < 7)
                {
                    Console.WriteLine("Arguments illegaux pour --convert, pour convertir utiliser '-c [-v <verboselevel>] <sourceformat> <sourcepath> <destformat> <destpath>'");
                    stop = true;
                }
                else
                {
                    switch (args[i + 1])
                    {
                        case "1":
                            verbose = IntegerType.FromString(args[i + 1]);
                            i += 2;
                            break;
                        case "2":
                            verbose = IntegerType.FromString(args[i + 1]);
                            i += 2;
                            break;
                        case "3":
                            verbose = IntegerType.FromString(args[i + 1]);
                            i += 2;
                            break;
                        default:
                            Console.WriteLine("Niveau de verbose invalide pour --verbose, les niveaux disponibles sont 1, 2 ou 3");
                            stop = true;
                            break;
                    }
                }
            }
            if (!stop) Conversion.convertFile(args[i], args[i + 1], args[i + 2], args[i + 3],verbose);
        }

        private static void analyzeFile(string[] args)
        {
            if (args.Length < 3)
                Console.WriteLine("Arguments illegaux pour --analyze, pour analyser utiliser '-a <sourceformat> <sourcepath>'");
            else Analyze.analyzeFile(args[1], args[2]);
        }

        private static void showHelp()
        {
            Console.WriteLine("\nUsage :\nAMC2MOODLE   [-c|--convert] [-v|--verbose] <verboselevel> <sourceformat> <sourcepath> <destformat> <destpath> \n" +
                "             [-h|--help]\n" +
                "             [-a|--analyze] <sourceformat> <sourcepath>\n\n" +
                "Detaille des commandes :\n\n" +
                "-c --convert\n\nUtilisé pour convertir un fichier au format<sourceformat> se situant à l'emplacement <sourcepath> vers un fichier au format<destformat> à l'emplacement <destpath>" +
                "\n------------------------------------------\n-h --help\n\nAffiche le manuel" +
                "\n------------------------------------------\n-a --analyze\n\nUtilisé pour analyser un fichier à l'emplacement <sourcepath> et verifier s'il correspond au format <sourceformat> specifié" +
                "\n------------------------------------------\n-v --verbose\n\nUtilisé pour specifier le niveau de log à utiliser pour --convert\npar défaut à 1 et possède 3 niveaux (1,2,3)" +
                "\n------------------------------------------");
        }
    }
}
