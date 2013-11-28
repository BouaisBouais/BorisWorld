using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Viking : Unite
    {

        public void deplacement(Case c, int x, int y)
        {
            int d = distance(x, y);

            if (mouvement >= d)
            {
                mouvement -= d;
                doDeplacement(x, y);
            }
        }

    }
}
