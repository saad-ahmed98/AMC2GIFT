using System;

namespace AMC2GIFT
{
    class Analyze
    {
        public static void analyzeFile(String sourceformat, String sourcepath)
        {
            Console.WriteLine("Debut de l'analyse du fichier "+sourcepath+" au format "+sourceformat+"...");
            switch (sourceformat.ToUpper())
            {
                case "AMC":
                    AMC.analyzeFile(sourcepath);
                    break;
                case "XMLMOODLE":
                    XMLMOODLE.analyzeFile(sourcepath);
                    break;
                case "GIFT":
                    GIFT.analyzeFile(sourcepath);
                    break;
                default:
                    Console.WriteLine("Arguments illegaux pour --analyze, aucun format de ce genre est supporté!");
                    break;
            }
        }
    }
}
