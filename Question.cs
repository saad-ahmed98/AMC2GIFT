using System;
using System.Collections.Generic;

namespace AMC2GIFT
{
    public class Question
    {
        public String nomq="";
        public List<String> titre = new List<String>();
        public List<Reponse> rps = new List<Reponse>();
        public String feedbackQ="";

        public void addReponse(Reponse rep)
        {
            rps.Add(rep);
        }

        public void addTitre(String txt)
        {
            titre.Add(txt);
        }

        public List<Reponse> getReponses()
        {
            return rps;
        }

        public String getTitre()
        {
            String res ="";
            foreach(String txt in titre)
            {
                res += txt;
            }
            return res;
        }
    }
}