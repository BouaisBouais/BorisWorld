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

        public Unite(int id, Coordonnee coords)
        {
            attaque = 2;
            defense = 1;
            vie = 2;
            mouvement = 1;

            idJoueur = id;
            coordonnees.clone(coords);
        }


        /// <summary>
        /// Regarde en fonction de la case si l'unité peut se déplacer ou pas
        /// </summary>
        public abstract void deplacement(Case @case, Coordonnee coords);
        public abstract void getPoints();


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
