using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    [Serializable]
    public class Viking : Unite
    {
        public Viking(Coordonnee coords) : base(coords)
        {
        }

        public override int getPoints() {
            if (Carte.getCase(coordonnees).getTypeCase() == TypeCases.EAU)
                return 0;
            if (Carte.bordEau(coordonnees))
                return 2;
            if(Carte.getCase(coordonnees).getTypeCase() == TypeCases.DESERT)
                return 0;
            return 1;
        }

        public override bool deplacementPossible(Coordonnee coords)
        {
            if (this.coordonnees.Equals(coords))
                return false;

            int distance = coordonnees.distance(coords);
            if (distance > 1) return false;
            return (mouvement >= 1);
        }

        public override resultatCombat deplacement(Coordonnee coords)
        {
            if (!deplacementPossible(coords)) return resultatCombat.DEPLACEMENT_IMPOSSIBLE;

            resultatCombat retour = verifUniteCase(coords);

            mouvement -= 1.0;
            makeResultatCombat(retour, coords);

            return retour;
        }

    }
}
