using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    abstract public class Unite
    {
        private int[] coordonnees;
        private int attaque;
        private int defense;
        private int vie;
        private int mouvement;

        /// <summary>
        /// Regarde en fonction de la case si l'unité peut se déplacer ou pas
        /// </summary>
        void deplacement(Case @case)
        {
        }

        int getPoints()
        {
            return 0;
        }

        void passerTour()
        {
        }

        void combattre(Unite @unite)
        {
        }
    }
}
