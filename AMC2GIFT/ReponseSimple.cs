using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AMC2GIFT
{
    class ReponseSimple : Reponse
    {
        public String texte;
        public Boolean vrai;

        public ReponseSimple(String fR, String txt, Boolean v)
        {
            this.feedbackR = fR;
            texte = txt;
            vrai = v;
        }
        
        public Boolean isTrue()
        {
            return vrai;
        }
    }
}
