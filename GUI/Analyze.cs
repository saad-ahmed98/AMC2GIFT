using System;
using System.Windows.Forms;

namespace AMC2GIFT_GUI
{
    class Analyze
    {
        public static TextBox logbox = null;
        public static void analyzeFile(String sourceformat, String sourcepath, TextBox textBox4)
        {
            logbox = textBox4;
            logbox.Text="Demarrage de l'analyse du fichier "+sourcepath+" au format "+sourceformat+Environment.NewLine + Environment.NewLine;
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
            }
            logbox.Text += Environment.NewLine+"Fin de l'analyse!";

            MessageBox.Show("Analyse terminée!");

        }
    }
}
