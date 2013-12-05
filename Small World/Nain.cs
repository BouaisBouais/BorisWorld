using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Nain : Unite
    {
        public Nain(int id, Coordonnee coords) : base (id, coords)
        {
        }

        public override int getPoints() {
            switch (Carte.getCase(coordonnees).getTypeCase())
            {
                case typeCases.FORET:
                    return 2;
                case typeCases.PLAINE:
                    return 0;
                default:
                    return 1;
            }
        }

        public override void deplacement(Case c, Coordonnee coords)
        {
            int d = coordonnees.distance(coords);

            if (
                (Carte.getCase(coordonnees).getTypeCase() == typeCases.MONTAGNE && c.getTypeCase() == typeCases.MONTAGNE && mouvement >=1 )
                || ((c.getTypeCase() != typeCases.EAU) && d == 1 && mouvement >= 1)
            )
            {
                mouvement--;
                doDeplacement(coords);
            }
        }

    }
}
