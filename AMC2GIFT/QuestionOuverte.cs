using System;
using System.Collections.Generic;
using System.Text;

namespace AMC2GIFT
{
    class QuestionOuverte : Question
    {
        int nblignes = 2;
        public void setLignes (int i)
        {
            nblignes = i;
        }

        public int getLignes()
        {
            return nblignes;
        }
    }
}
