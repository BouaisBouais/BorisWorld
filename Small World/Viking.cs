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

        public override int getPoints() {

            if (Carte.getCase(coordonnees).getTypeCase() == TypeCases.EAU) {
                return 0;
            } else {

                if (Carte.bordEau(coordonnees))
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }

        
        }

        public override bool deplacementPossible(Coordonnee coords)
        {
            int distance = coordonnees.distance(coords);
            if (distance > 1) return false;
            return (mouvement >= 1);
        }

        public override bool deplacement(Coordonnee coords)
        {
            if (!deplacementPossible(coords)) return false;

            verifUniteCase(coords);

            mouvement -= 1.0;
            return true;
        }

    }
}
