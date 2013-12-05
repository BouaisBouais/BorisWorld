using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    abstract public class Unite
    {
        protected Coordonnee coordonnees = new Coordonnee();
        protected int attaque;
        protected int defense;
        protected int vie;
        protected double mouvement;
        protected int idJoueur;

        public static const int MOUVEMENT_MAX = 1;
        public static const int ATTAQUE_MAX = 2;
        public static const int VIE_MAX = 2;
        public static const int DEFENSE_MAX = 1;


        public Unite(int id, Coordonnee coords)
        {
            attaque = ATTAQUE_MAX;
            defense = DEFENSE_MAX;
            vie = VIE_MAX;
            mouvement = MOUVEMENT_MAX;

            idJoueur = id;
            coordonnees.clone(coords);
        }


        /// <summary>
        /// Regarde en fonction de la case si l'unité peut se déplacer ou pas
        /// </summary>
        public abstract void deplacement(Case @case, Coordonnee coords);
        public abstract int getPoints();


        public void doDeplacement(Coordonnee newCoords)
        {
            coordonnees.clone(newCoords);
        }

        public void passerTour()
        {
        }

        public void combattre(Unite @unite)
        {
        }
    }
}
