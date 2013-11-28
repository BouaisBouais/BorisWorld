using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Viking : Unite
    {
        public Viking(int id, Coordonnee coords) : base(id, coords)
        {
        }

        public override void getPoints() { }

        public override void deplacement(Case c, Coordonnee coords)
        {
            int d = coordonnees.distance(coords);

            if (mouvement >= d)
            {
                mouvement -= d;
                doDeplacement(coords);
            }
        }

    }
}
