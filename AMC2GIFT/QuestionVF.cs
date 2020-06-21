using System;
using System.Collections.Generic;
using System.Text;

namespace AMC2GIFT
{
    class QuestionVF : QuestionSimple
    {

        public String getTrueOrFalse()
        {
            String res = "F";
            foreach (ReponseSimple r in this.getReponses())
            {
                if (r.texte.ToLower().Equals("true") && r.isTrue()) res = "T";
            }
            return res;
        }
    }
}
