using System;


namespace AMC2GIFT_GUI
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
