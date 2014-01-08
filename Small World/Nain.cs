﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    [Serializable]
    public class Nain : Unite
    {
        public Nain(Coordonnee coords) : base (coords)
        {
        }

        public override int getPoints() {
            switch (Carte.getCase(coordonnees).getTypeCase())
            {
                case TypeCases.FORET:
                    return 2;
                case TypeCases.PLAINE:
                    return 0;
                default:
                    return 1;
            }
        }

        public override resultatCombat deplacement(Coordonnee coords)
        {
            if (!deplacementPossible(coords)) return resultatCombat.DEPLACEMENT_IMPOSSIBLE;

            resultatCombat retour = verifUniteCase(coords);

            mouvement -= 1.0;
            makeResultatCombat(retour, coords);

            return retour;
        }

        public override bool deplacementPossible(Coordonnee coords)
        {
            if (this.coordonnees.Equals(coords))
                return false;

            int distance = coordonnees.distance(coords);
            Case c = Carte.getCase(coords);

            if (c.getTypeCase() == TypeCases.EAU) return false;

            Case currentCase = Carte.getCase(coordonnees);
            if (currentCase.getTypeCase() == TypeCases.MONTAGNE && c.getTypeCase() == TypeCases.MONTAGNE && mouvement >= 1) return true;

            if (distance > 1) return false;

            return (mouvement >= 1);
        }

    }
}
