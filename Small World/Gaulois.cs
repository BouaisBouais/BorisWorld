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
        
        public override int getPoints() {
            switch (Carte.getCase(coordonnees).getTypeCase())
            {
                case typeCases.PLAINE:
                    return 2;
                case typeCases.MONTAGNE:
                    return 0;
                default:
                    return 1;
            }
        }


        public override  void deplacement(Case c, Coordonnee coords)
        {
            int d = coordonnees.distance(coords);

            // A revoir (CH : en effet, il faut toujours ne se déplacer que d'une case. Si la case entre les deux
            // n'est pas une plaine, le déplacement est de 1.5, pas 1. Donc il faut qu'on fonctionne par étapes.)
            if (c.getTypeCase() ==  typeCases.PLAINE && d == 1 && mouvement >= 0.5)
            {
                mouvement -= 0.5;
                doDeplacement(coords);
            }
            else if (!(c.getTypeCase() ==  typeCases.EAU) && d == 1 && mouvement >= 1)
            {
                mouvement -= d;
                doDeplacement(coords);
            }
        }


    }
}
