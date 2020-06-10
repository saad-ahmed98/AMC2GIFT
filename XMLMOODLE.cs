using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;


namespace AMC2MOODLE
{
    class XMLMOODLE
    {
        internal static void analyzeFile(string v)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();

                settings.Schemas.Add(null, "resources/schema xml moodle.xsd");
                settings.ValidationType = ValidationType.Schema;

                XmlReader reader = XmlReader.Create(v, settings);
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                document.Validate(null);
                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
