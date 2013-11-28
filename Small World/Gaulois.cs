using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Gaulois : Unite
    {


        public void deplacement(Case c, int x, int y)
        {
            int d = distance(x, y);

            // A revoir
            if (c is CasePlaine && mouvement >= (double) d/2)
            {
                mouvement -= (double) d/2;
                doDeplacement(x, y);

            }
            else if (!(c is CaseEau) && mouvement >= d)
            {
                mouvement -= d;
                doDeplacement(x, y);
            }
        }


    }
}
