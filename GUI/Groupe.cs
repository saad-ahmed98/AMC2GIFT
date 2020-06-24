using System;
using System.Collections.Generic;

namespace AMC2GIFT_GUI
{
    public class Groupe
    {
        public List<String> texte = new List<string>();
        public List<Question> qt = new List<Question>();

        public Groupe()
        {
            texte.Add("");
            texte.Add("");
        }

        public void addTexte1(String txt)
        {
            texte[1] = txt;
        }
        public void addTexte2(String txt)
        {
            texte[2]=txt;
        }

        public void addQuestion(Question q)
        {
            qt.Add(q);
        }
        public List<Question> getQuestions() {
            return qt;
        }

        public List<String> getTexte()
        {
            return texte;
        }
    }
}