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
            int nbPointNorm;
            switch (SmallWorld.Instance.carte.getCase(coordonnees).getTypeCase())
            {
                case TypeCases.DESERT:
                    nbPointNorm = 0;
                    break;
                case TypeCases.EAU:
                case TypeCases.VORTEX:
                    return 0;
                default:
                    nbPointNorm = 1;
                    break;
            }

            if (SmallWorld.Instance.carte.bordEau(coordonnees))
                nbPointNorm++;

            return nbPointNorm;
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

        public override TypeUnite getPeuple()
        {
            return TypeUnite.Viking;
        }

    }
}
