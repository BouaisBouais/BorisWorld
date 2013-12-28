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

        public override bool deplacementPossible(Coordonnee coords)
        {
            Case c = Carte.getCase(coords);
            int distance = coordonnees.distance(coords);
            if (distance > 1 || c.getTypeCase() == typeCases.EAU) return false;

            if (c.getTypeCase() == typeCases.PLAINE && mouvement >= 0.5) return true;

            return (mouvement >= 1);
        }

        public override bool deplacement(Coordonnee coords)
        {
            if (!deplacementPossible(coords)) return false;

            Case c = Carte.getCase(coords);
            if (c.getTypeCase() == typeCases.PLAINE) mouvement -= 0.5;
            else mouvement -= 1.0;

            return true;
        }


    }
}
