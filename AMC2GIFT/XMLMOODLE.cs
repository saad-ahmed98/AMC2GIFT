using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;


namespace AMC2GIFT
{
    class XMLMOODLE
    {
        internal static void analyzeFile(string v)
        {
            try
            {
                String xsd = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4KPCEtLSBDcmVhdGVkIHdpdGggTGlxdWlkIFRlY2hub2xvZ2llcyBPbmxpbmUgVG9vbHMgMS4wIChodHRwczovL3d3dy5saXF1aWQtdGVjaG5vbG9naWVzLmNvbSkgLS0+Cjx4czpzY2hlbWEgYXR0cmlidXRlRm9ybURlZmF1bHQ9InVucXVhbGlmaWVkIiBlbGVtZW50Rm9ybURlZmF1bHQ9InF1YWxpZmllZCIKICAgIHhtbG5zOnhzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYSI+CiAgICA8eHM6ZWxlbWVudCBuYW1lPSJxdWl6Ij4KICAgICAgICA8eHM6Y29tcGxleFR5cGU+CiAgICAgICAgICAgIDx4czpzZXF1ZW5jZT4KICAgICAgICAgICAgICAgIDx4czplbGVtZW50IG1heE9jY3Vycz0idW5ib3VuZGVkIiBuYW1lPSJxdWVzdGlvbiI+CiAgICAgICAgICAgICAgICAgICAgPHhzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgICAgICAgICA8eHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6ZWxlbWVudCBtaW5PY2N1cnM9IjAiIG1heE9jY3Vycz0idW5ib3VuZGVkIiBuYW1lPSJzdWJxdWVzdGlvbiI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6ZWxlbWVudCBuYW1lPSJ0ZXh0IiB0eXBlPSJ4czpzdHJpbmciLz4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx4czplbGVtZW50IG5hbWU9ImFuc3dlciI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmVsZW1lbnQgbmFtZT0idGV4dCIgdHlwZT0ieHM6c3RyaW5nIi8+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czpjb21wbGV4VHlwZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6ZWxlbWVudD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czpzZXF1ZW5jZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3hzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czplbGVtZW50PgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmVsZW1lbnQgbWluT2NjdXJzPSIwIiBuYW1lPSJuYW1lIj4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6Y29tcGxleFR5cGU+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx4czpzZXF1ZW5jZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx4czplbGVtZW50IG5hbWU9InRleHQiIHR5cGU9InhzOnN0cmluZyIvPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3hzOnNlcXVlbmNlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6Y29tcGxleFR5cGU+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3hzOmVsZW1lbnQ+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6ZWxlbWVudCBtaW5PY2N1cnM9IjAiIG5hbWU9InF1ZXN0aW9udGV4dCI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6ZWxlbWVudCBuYW1lPSJ0ZXh0IiB0eXBlPSJ4czpzdHJpbmciLz4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czpzZXF1ZW5jZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmF0dHJpYnV0ZSBuYW1lPSJmb3JtYXQiIHR5cGU9InhzOnN0cmluZyIgdXNlPSJyZXF1aXJlZCIvPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6Y29tcGxleFR5cGU+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3hzOmVsZW1lbnQ+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6ZWxlbWVudCBtaW5PY2N1cnM9IjAiIG5hbWU9ImdlbmVyYWxmZWVkYmFjayI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6ZWxlbWVudCBuYW1lPSJ0ZXh0IiB0eXBlPSJ4czpzdHJpbmciLz4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czpzZXF1ZW5jZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3hzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czplbGVtZW50PgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmVsZW1lbnQgbWluT2NjdXJzPSIwIiBtYXhPY2N1cnM9InVuYm91bmRlZCIgbmFtZT0iYW5zd2VyIj4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6Y29tcGxleFR5cGU+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx4czpzZXF1ZW5jZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx4czplbGVtZW50IG5hbWU9InRleHQiIHR5cGU9InhzOnN0cmluZyIvPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmVsZW1lbnQgbWluT2NjdXJzPSIwIiBuYW1lPSJmZWVkYmFjayI+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmVsZW1lbnQgbmFtZT0idGV4dCIgdHlwZT0ieHM6c3RyaW5nIi8+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czpjb21wbGV4VHlwZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6ZWxlbWVudD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czpzZXF1ZW5jZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmF0dHJpYnV0ZSBuYW1lPSJmcmFjdGlvbiIgdHlwZT0ieHM6ZGVjaW1hbCIgdXNlPSJyZXF1aXJlZCIvPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6Y29tcGxleFR5cGU+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3hzOmVsZW1lbnQ+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8eHM6ZWxlbWVudCBtaW5PY2N1cnM9IjAiIG5hbWU9InVzZWNhc2UiIHR5cGU9InhzOnVuc2lnbmVkQnl0ZSIvPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmVsZW1lbnQgbWluT2NjdXJzPSIwIiBuYW1lPSJzaHVmZmxlYW5zd2VycyIgdHlwZT0ieHM6Ym9vbGVhbiIvPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmVsZW1lbnQgbWluT2NjdXJzPSIwIiBuYW1lPSJzaW5nbGUiIHR5cGU9InhzOmJvb2xlYW4iLz4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx4czplbGVtZW50IG1pbk9jY3Vycz0iMCIgbmFtZT0iYW5zd2VybnVtYmVyaW5nIiB0eXBlPSJ4czpzdHJpbmciLz4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx4czplbGVtZW50IG1pbk9jY3Vycz0iMCIgbmFtZT0iY2F0ZWdvcnkiPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx4czpjb21wbGV4VHlwZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOnNlcXVlbmNlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmVsZW1lbnQgbmFtZT0idGV4dCIgdHlwZT0ieHM6c3RyaW5nIi8+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6c2VxdWVuY2U+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPC94czpjb21wbGV4VHlwZT4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwveHM6ZWxlbWVudD4KICAgICAgICAgICAgICAgICAgICAgICAgPC94czpzZXF1ZW5jZT4KICAgICAgICAgICAgICAgICAgICAgICAgPHhzOmF0dHJpYnV0ZSBuYW1lPSJ0eXBlIiB0eXBlPSJ4czpzdHJpbmciIHVzZT0icmVxdWlyZWQiLz4KICAgICAgICAgICAgICAgICAgICA8L3hzOmNvbXBsZXhUeXBlPgogICAgICAgICAgICAgICAgPC94czplbGVtZW50PgogICAgICAgICAgICA8L3hzOnNlcXVlbmNlPgogICAgICAgIDwveHM6Y29tcGxleFR5cGU+CiAgICA8L3hzOmVsZW1lbnQ+CjwveHM6c2NoZW1hPgo=";

                Byte[] bytes = Convert.FromBase64String(xsd);
                XmlReaderSettings settings = new XmlReaderSettings();
                MemoryStream ms = new MemoryStream(bytes);

                settings.Schemas.Add(null, XmlReader.Create(ms));
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

        internal static void logExport(Question q)
        {
            Console.WriteLine("Exportation de question de type " + q.GetType().Name + "...");
            Console.WriteLine("Titre question : " + q.getTitre());
            Console.WriteLine("Nombre de reponses : " + q.getReponses().Count);
        }
        internal static void logImport(Question q)
        {
            Console.WriteLine("Lecture de question de type " + q.GetType().Name + "...");
            Console.WriteLine("Titre question : " + q.getTitre());
            Console.WriteLine("Nombre de reponses : " + q.getReponses().Count);
        }

        internal static List<Groupe> importQuestions(string sourcepath, int verbose)
        {
            XmlDocument document = new XmlDocument();
            document.Load(sourcepath);
            List<Groupe> res = new List<Groupe>();
            Groupe g = new Groupe();
            foreach (XmlNode question in document.DocumentElement)
            {
                Question q = null;
                if (question.Name.Equals("question"))
                {
                    switch (question.Attributes[0].InnerText)
                    {
                        case "multichoice":
                            q = makeMultiChoiceQuestion(question);
                            break;
                        case "description":
                            q = makecDescription(question);
                            break;
                        case "essay":
                            q = makeEssayQuestion(question);
                            break;
                        case "matching":
                            q = makeMatchingQuestion(question);
                            break;
                        case "truefalse":
                            q = makeTrueFalseQuestion(question);
                            break;
                        case "numerical":
                            q = makeNumericalQuestion(question);
                            break;
                        case "shortanswer":
                            q = makeShortAnswerQuestion(question);
                            break;
                    }
                    if (q != null)
                    {
                        g.addQuestion(q);
                        logImport(q);
                    }
                }
            }
            res.Add(g);
            return res;
        }

        private static Question makeMatchingQuestion(XmlNode question)
        {
            Question res = new QuestionAppariement();
            String titreq = "";
            String feedbackq = "";
            String nom = "";
            if (question.SelectSingleNode("name") != null)
                nom = question["name"].FirstChild.InnerText;
            res.nomq = nom;
            if (question.SelectSingleNode("questiontext") != null)
                titreq = question["questiontext"].FirstChild.InnerText;
            if (question.SelectSingleNode("generalfeedback") != null)
                feedbackq = question["generalfeedback"].FirstChild.InnerText;
            foreach (XmlNode reponse in question.SelectNodes("subquestion"))
            {
                res.addReponse(new ReponseMatching(reponse.FirstChild.InnerText, reponse["answer"].FirstChild.InnerText));
            }
            res.addTitre(titreq);
            res.feedbackQ = feedbackq;
            return res;
        }

        private static Question makecDescription(XmlNode question)
        {
            Question res = new QuestionDescription();
            String titreq = "";
            String nom = "";
            if (question.SelectSingleNode("name") != null)
                nom = question["name"].FirstChild.InnerText;
            res.nomq = nom;
            if (question.SelectSingleNode("questiontext") != null)
                titreq = question["questiontext"].FirstChild.InnerText;
            res.addTitre(titreq);
            return res;
        }

        private static Question makeEssayQuestion(XmlNode question)
        {
            Question res = new QuestionEssay();
            String titreq = "";
            String feedbackq = "";
            String nom = "";
            if (question.SelectSingleNode("name") != null)
                nom = question["name"].FirstChild.InnerText;
            res.nomq = nom;
            if (question.SelectSingleNode("questiontext") != null)
                titreq = question["questiontext"].FirstChild.InnerText;
            if (question.SelectSingleNode("generalfeedback") != null)
                feedbackq = question["generalfeedback"].FirstChild.InnerText;
            foreach (XmlNode reponse in question.SelectNodes("answer"))
            {
                String feedbackr = "";
                Boolean vrai = false;
                if (reponse.SelectSingleNode("feedback") != null)
                    feedbackr = reponse["feedback"].FirstChild.InnerText;
                if (double.Parse(reponse.Attributes[0].InnerText.Replace(".", ",")) > 0)
                    vrai = true;
                res.addReponse(new ReponseSimple(feedbackr, reponse["text"].InnerText, vrai));
            }
            res.addTitre(titreq);
            res.feedbackQ = feedbackq;
            return res;
        }

        private static Question makeShortAnswerQuestion(XmlNode question)
        {
            Question res = new QuestionShort();
            String titreq = "";
            String feedbackq = "";
            String nom = "";
            if (question.SelectSingleNode("name") != null)
                nom = question["name"].FirstChild.InnerText;
            res.nomq = nom;
            if (question.SelectSingleNode("questiontext") != null)
                titreq = question["questiontext"].FirstChild.InnerText;
            if (question.SelectSingleNode("generalfeedback") != null)
                feedbackq = question["generalfeedback"].FirstChild.InnerText;
            foreach (XmlNode reponse in question.SelectNodes("answer"))
            {
                String feedbackr = "";
                Boolean vrai = false;
                if (reponse.SelectSingleNode("feedback") != null)
                    feedbackr = reponse["feedback"].FirstChild.InnerText;
                if (double.Parse(reponse.Attributes[0].InnerText.Replace(".", ",")) > 0)
                    vrai = true;
                res.addReponse(new ReponseSimple(feedbackr, reponse["text"].InnerText, vrai));
            }
            res.addTitre(titreq);
            res.feedbackQ = feedbackq;
            return res;
        }

        private static Question makeNumericalQuestion(XmlNode question)
        {
            Question res = new QuestionNumerical();
            String titreq = "";
            String feedbackq = "";
            String nom = "";
            if (question.SelectSingleNode("name") != null)
                nom = question["name"].FirstChild.InnerText;
            res.nomq = nom;
            if (question.SelectSingleNode("questiontext") != null)
                titreq = question["questiontext"].FirstChild.InnerText;
            if (question.SelectSingleNode("generalfeedback") != null)
                feedbackq = question["generalfeedback"].FirstChild.InnerText;
            foreach (XmlNode reponse in question.SelectNodes("answer"))
            {
                String feedbackr = "";
                Boolean vrai = false;
                if (reponse.SelectSingleNode("feedback") != null)
                    feedbackr = reponse["feedback"].FirstChild.InnerText;
                if (double.Parse(reponse.Attributes[0].InnerText.Replace(".", ",")) > 0)
                    vrai = true;
                res.addReponse(new ReponseSimple(feedbackr, reponse["text"].InnerText, vrai));
            }
            res.addTitre(titreq);
            res.feedbackQ = feedbackq;
            return res;
        }

        private static Question makeTrueFalseQuestion(XmlNode question)
        {
            Question res = new QuestionVF();
            String titreq = "";
            String feedbackq = "";
            String nom = "";
            if (question.SelectSingleNode("name") != null)
                nom = question["name"].FirstChild.InnerText;
            res.nomq = nom;
            if (question.SelectSingleNode("questiontext") != null)
                titreq = question["questiontext"].FirstChild.InnerText;
            if (question.SelectSingleNode("generalfeedback") != null)
                feedbackq = question["generalfeedback"].FirstChild.InnerText;
            foreach (XmlNode reponse in question.SelectNodes("answer"))
            {
                String feedbackr = "";
                Boolean vrai = false;
                if (reponse.SelectSingleNode("feedback") != null)
                    feedbackr = reponse["feedback"].FirstChild.InnerText;
                if (double.Parse(reponse.Attributes[0].InnerText.Replace(".", ",")) > 0)
                    vrai = true;
                res.addReponse(new ReponseSimple(feedbackr, reponse["text"].InnerText, vrai));
            }
            res.addTitre(titreq);
            res.feedbackQ = feedbackq;
            return res;
        }

        private static Question makeMultiChoiceQuestion(XmlNode question)
        {
            Question res;
            String titreq = "";
            String feedbackq = "";
            if (question["single"].InnerText.Equals("true"))
                res = new QuestionSimple();
            else res = new QuestionMultiple();
            String nom = "";
            if (question.SelectSingleNode("name") != null)
                nom = question["name"].FirstChild.InnerText;
            res.nomq = nom;
            if (question.SelectSingleNode("questiontext") != null)
                titreq = question["questiontext"].FirstChild.InnerText;
            if (question.SelectSingleNode("generalfeedback") != null)
                feedbackq = question["generalfeedback"].FirstChild.InnerText;
            foreach (XmlNode reponse in question.SelectNodes("answer"))
            {
                String feedbackr = "";
                Boolean vrai = false;
                if (reponse.SelectSingleNode("feedback") != null)
                    feedbackr = reponse["feedback"].FirstChild.InnerText;
                if (double.Parse(reponse.Attributes[0].InnerText.Replace(".", ",")) > 0)
                    vrai = true;
                res.addReponse(new ReponseSimple(feedbackr, reponse["text"].InnerText, vrai));
            }
            res.addTitre(titreq);
            res.feedbackQ = feedbackq;
            return res;
        }

        internal static void exportQuestions(List<Groupe> gquestions, string destpath, int verbose)
        {
            XmlTextWriter writer = new XmlTextWriter(destpath, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("quiz");
            foreach (Groupe g in gquestions)
            {
                int i = 1;
                foreach (Question q in g.getQuestions())
                {
                    logExport(q);
                    writer.WriteStartElement("question");
                    switch (q)
                    {
                        case QuestionDescription qd:
                            writer.WriteAttributeString("type", "description");
                            break;
                        case QuestionVF qvf:
                            writer.WriteAttributeString("type", "truefalse");
                            break;
                        case QuestionSimple qs:
                            writer.WriteAttributeString("type", "multichoice");
                            writer.WriteStartElement("single");
                            writer.WriteString("true");
                            writer.WriteEndElement();
                            break;
                        case QuestionMultiple qm:
                            writer.WriteAttributeString("type", "multichoice");
                            writer.WriteStartElement("single");
                            writer.WriteString("false");
                            writer.WriteEndElement();
                            break;
                        case QuestionAppariement qmt:
                            writer.WriteAttributeString("type", "matching");
                            break;
                        case QuestionNumerical qn:
                            writer.WriteAttributeString("type", "numerical");
                            break;
                        case QuestionShort qs:
                            writer.WriteAttributeString("type", "shortanswer");
                            break;
                        default:
                            writer.WriteAttributeString("type", "essay");
                            break;

                    }
                    if (q.nomq.Equals(""))
                        q.nomq = "Question " + i;
                    writer.WriteStartElement("name");
                    writer.WriteStartElement("text");
                    writer.WriteString(q.nomq);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    if (q.getTitre().Equals(""))
                    {
                        q.titre[0] = "No title";
                    }

                    writer.WriteStartElement("questiontext");
                    writer.WriteStartElement("text");
                    writer.WriteString(q.getTitre());
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    if (!q.feedbackQ.Equals(""))
                    {
                        writer.WriteStartElement("generalfeedback");
                        writer.WriteStartElement("text");
                        writer.WriteString(q.feedbackQ);
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    if (q is QuestionOuverteAMC)
                    {
                        q.getReponses().Clear();
                        //q.addReponse(new ReponseSimple("", "", false));
                    }
                    foreach (Reponse r in q.getReponses())
                    {
                        if (r is ReponseSimple)
                        {
                            String fraction;
                            ReponseSimple rs = (ReponseSimple)r;
                            if (rs.isTrue()) fraction = "100";
                            else fraction = "0";
                            writer.WriteStartElement("answer");
                            writer.WriteAttributeString("fraction", fraction);
                            writer.WriteStartElement("text");
                            writer.WriteString(rs.texte);
                            writer.WriteEndElement();
                            if (!rs.feedbackR.Equals(""))
                            {
                                writer.WriteStartElement("feedback");
                                writer.WriteStartElement("text");
                                writer.WriteString(rs.feedbackR);
                                writer.WriteEndElement();
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        if (r is ReponseMatching)
                        {
                            ReponseMatching rm = (ReponseMatching)r;
                            writer.WriteStartElement("subquestion");
                            writer.WriteStartElement("text");
                            writer.WriteString(rm.texte1);
                            writer.WriteEndElement();
                            writer.WriteStartElement("answer");
                            writer.WriteStartElement("text");
                            writer.WriteString(rm.texte2);
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                    i++;
                }

            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}

