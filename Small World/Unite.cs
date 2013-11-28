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
        protected double mouvement;
        private int idJoueur;

        public Unite(int id, int x, int y)
        {
            attaque = 2;
            defense = 1;
            vie = 2;
            mouvement = 1;

            idJoueur = id;
            coordonnees[0] = x;
            coordonnees[1] = y;
        }


        /// <summary>
        /// Regarde en fonction de la case si l'unité peut se déplacer ou pas
        /// </summary>
        abstract public void deplacement(Case @case,int x,int y);
        abstract public void getPoints();


        public void doDeplacement(int x, int y)
        {
            coordonnees[0] = x;
            coordonnees[1] = y;
        }
        public void passerTour()
        {
        }

        public void combattre(Unite @unite)
        {
        }

        //renvoie la distance entre les coordonnées de l'unité et celles passées en paramétre
        protected int distance(int x, int y)
        {
            int newX = Math.Abs(coordonnees[0] - x);
            int newY = Math.Abs(coordonnees[1] - y);


            return newX + newY;

        }
    }
}
