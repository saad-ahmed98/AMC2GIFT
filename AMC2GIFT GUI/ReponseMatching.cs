using System;


namespace AMC2GIFT_GUI
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
