using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AMC2GIFT
{
    class ReponseMatching : Reponse
    {
        public String texte1;
        public String texte2;

        public ReponseMatching(String txt1, String txt2)
        {
            texte1 = txt1;
            texte2 = txt2;
        }
    }
}
