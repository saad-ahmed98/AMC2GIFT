using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AMC2GIFT_GUI
{
    class Conversion
    {

        public static TextBox logbox = null;
        public static List<Groupe> importQuestions(String sourceformat, String sourcepath, int verbose)
        {
            logbox.Text = "";
            logbox.AppendText("Demarrage lecture des questions...");
            logbox.AppendText(Environment.NewLine);
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
            }
            logbox.AppendText(Environment.NewLine);
            logbox.AppendText("Fin lecture des questions!");
            logbox.AppendText(Environment.NewLine);
            return resImport;
        }

        public static void exportQuestions(String destformat, String destpath, List<Groupe> gquestions, int verbose)
        {
            logbox.AppendText(Environment.NewLine);
            logbox.AppendText("Demarrage exportation des questions!");
            logbox.AppendText(Environment.NewLine);
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
            }
            logbox.AppendText(Environment.NewLine);
            logbox.AppendText("Fin exportation des questions!");
            logbox.AppendText(Environment.NewLine);
        }

        public static void convertFile(String sourceformat, String sourcepath, String destformat, String destpath, System.Windows.Forms.TextBox textBox3, int verbose)
        {
            logbox = textBox3;
            List<Groupe> resImport = importQuestions(sourceformat, sourcepath, verbose);
            if (resImport.Count >= 1) exportQuestions(destformat, destpath, resImport, verbose);
            MessageBox.Show("Conversion terminée!");

        }
    }
}
