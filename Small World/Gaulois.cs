using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Gaulois : Unite
    {
        public Gaulois(int id, Coordonnee coords) : base(id, coords)
        {
        }

        public override void getPoints() { }


        public override  void deplacement(Case c, Coordonnee coords)
        {
            int d = coordonnees.distance(coords);

            // A revoir (CH : en effet, il faut toujours ne se déplacer que d'une case. Si la case entre les deux
            // n'est pas une plaine, le déplacement est de 1.5, pas 1. Donc il faut qu'on fonctionne par étapes.)
            if (c is CasePlaine && mouvement >= (double) d/2)
            {
                mouvement -= (double) d/2;
                doDeplacement(coords);

            }
            else if (!(c is CaseEau) && mouvement >= d)
            {
                mouvement -= d;
                doDeplacement(coords);
            }
        }


    }
}
