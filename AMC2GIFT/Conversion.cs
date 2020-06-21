using System;
using System.Collections.Generic;

namespace AMC2GIFT
{
    class Conversion
    {

        public static List<Groupe> importQuestions(String sourceformat, String sourcepath, int verbose)
        {
            Console.WriteLine("Start importing...");
            List<Groupe> resImport= new List<Groupe>();
            switch (sourceformat.ToUpper())
            {
                case "AMC":
                    resImport.AddRange(AMC.importQuestions(sourcepath, verbose));
                    break;
                case "XMLMOODLE":
                    resImport.AddRange(XMLMOODLE.importQuestions(sourcepath, verbose));
                    break;
                case "GIFT":
                    resImport.AddRange(GIFT.importQuestions(sourcepath, verbose));
                    break;
                default:
                    Console.WriteLine("illegal arguments for --convert, no such import format supported");
                    break;
            }
            Console.WriteLine("Done importing!");
            return resImport;
        }

        public static void exportQuestions(String destformat, String destpath, List<Groupe> gquestions, int verbose)
        {
            Console.WriteLine("Start exporting...");
            switch (destformat.ToUpper())
            {
                case "AMC":
                    AMC.exportQuestions(gquestions, destpath, verbose);
                    break;
                case "XMLMOODLE":
                    XMLMOODLE.exportQuestions(gquestions, destpath, verbose);
                    break;
                case "GIFT":
                    GIFT.exportQuestions(gquestions, destpath, verbose);
                    break;
                default:
                    Console.WriteLine("illegal arguments for --convert, no such export format supported");
                    break;
            }
            Console.WriteLine("Done exporting!");
        }

        public static void convertFile(String sourceformat, String sourcepath, String destformat, String destpath, int verbose)
        {
            List<Groupe> resImport = importQuestions(sourceformat, sourcepath, verbose);
            if (resImport.Count >= 1) exportQuestions(destformat, destpath, resImport, verbose);
        }
    }
}
