using System;

namespace AMC2GIFT
{
    class Analyze
    {
        public static Boolean analyzeFile(String sourceformat, String sourcepath)
        {
            Boolean error = true;
            Console.WriteLine("Debut de l'analyse du fichier "+sourcepath+" au format "+sourceformat+"...");
            switch (sourceformat.ToUpper())
            {
                case "AMCTXT":
                    error = AMCTXT.analyzeFile(sourcepath);
                    break;
                case "XMLMOODLE":
                    error = XMLMOODLE.analyzeFile(sourcepath);
                    break;
                case "GIFT":
                    error = GIFT.analyzeFile(sourcepath);
                    break;
                default:
                    Console.WriteLine("Arguments illegaux pour --analyze, aucun format de ce genre est supporté!");
                    break;
            }
            if (error)
                Console.WriteLine("Analyse terminée avec des erreurs!");
            else Console.WriteLine("Analyse terminée sans erreurs!");
            return error;
        }
    }
}
