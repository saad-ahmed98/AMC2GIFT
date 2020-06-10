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
                Console.WriteLine("no arguments, please use -h or --help to check the manual.");
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
                        Console.WriteLine("illegal arguments, please use -h or --help to check the manual.");
                        break;
                }
        }

        private static void convertFile(string[] args)
        {
            int i = 1;
            Boolean stop = false;
            if (args.Length < 5)
            {
                Console.WriteLine("illegal arguments for --convert, to convert use '-c [-v <verboselevel>] <sourceformat> <sourcepath> <destformat> <destpath>'");
                stop = true;
            }
            else if (args[i].Equals("-v") || args[i].Equals("--verbose"))
            {
                if (args.Length < 7)
                {
                    Console.WriteLine("illegal arguments for --convert, to convert use '-c [-v <verboselevel>] <sourceformat> <sourcepath> <destformat> <destpath>'");
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
                            Console.WriteLine("illegal verbose level for --verbose, the verbose levels can be either 1, 2 or 3");
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
                Console.WriteLine("illegal arguments for --analyze, to analyze use '-a <sourceformat> <sourcepath>'");
            else Analyze.analyzeFile(args[1], args[2]);
        }

        private static void showHelp()
        {
            Console.WriteLine("\nUsage :\nAMC2MOODLE   [-c|--convert] [-v|--verbose] <verboselevel> <sourceformat> <sourcepath> <destformat> <destpath> \n" +
                "             [-h|--help]\n" +
                "             [-a|--analyze] <sourceformat> <sourcepath>\n\n" +
                "Detail of the commands :\n\n" +
                "-c --convert\n\nUsed to convert a <sourceformat> file at the <sourcepath> path to a <destformat> file at the <destpath>\nif no destination path is specified, then the current directory will be used by default" +
                "\n------------------------------------------\n-h --help\n\nShow the manual" +
                "\n------------------------------------------\n-a --analyze\n\nUsed to analyze a file at the <sourcepath> and check if it corresponds to the <sourceformat> specified" +
                "\n------------------------------------------\n-v --verbose\n\nUsed to specify the level of log showed when running a conversion\ndefault to 1 and has 3 levels (1,2,3)" +
                "\n------------------------------------------");
        }
    }
}
