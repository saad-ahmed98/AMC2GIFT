using System;

namespace AMC2GIFT
{
    class Analyze
    {
        public static void analyzeFile(String sourceformat, String sourcepath)
        {
            Console.WriteLine("Beginning to analyze file at "+sourcepath+" in "+sourceformat+" format...");
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
                    Console.WriteLine("Illegal arguments for --analyze, no such format supported, to analyze use '-a <sourceformat> <sourcepath>'");
                    break;
            }
        }
    }
}
