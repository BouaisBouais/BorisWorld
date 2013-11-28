using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Nain : Unite
    {
        private bool estSurMontagne = false;

        public void deplacement(Case c, int x, int y)
        {
            if (c is CaseMontagne)
            {
                mouvement -= 0.5;
                doDeplacement(x, y);

            }
            else if (!(c is CaseEau) && mouvement >= 1)
            {
                mouvement--;
                doDeplacement(x, y);
            }
        }

    }
}
