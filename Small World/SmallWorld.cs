using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{

    public class SmallWorld
    {
        private Carte carte;
        static public List<Joueur> joueurs {get; set;}
        static public int joueurCourant { get; private set; }
        static public int uniteCourante { get; private set; }
        static public int NOMBRE_JOUEURS = 2;


        private int nbTours = 0;
        private int nbTourMax;

        enum TypeAction { DEPLACEMENT, PASSER_TOUR };

        public SmallWorld()
        {
        }

        /**
         * Lance une nouvelle partie
         */
        public void nouvellePartie(TypeCarte tailleCarte, TypeUnite j1, TypeUnite j2)
        {
            MonteurPartie monteur = new MonteurPartie();
            monteur.nouvellePartie(tailleCarte, j1, j2);
            nbTourMax = Carte.getNombreTours();
        }

        /**
         * Demande à une unité de se déplacer sur la case visée
         */
        public void deplacement(Coordonnee c)
        {
            resultatCombat result = getUniteCourante().deplacement(c);
            if (result == resultatCombat.ATTAQUANT_MORT || getUniteCourante().mouvement == 0)
            {
                int nextUnite = getJoueurCourant().getFirstMovementAbleUnit();
                if (nextUnite == -1)
                    passerTour();
                else
                    uniteCourante = nextUnite;
            }
            checkFinJeu();
        }

        /**
         * Fais passer l'unité courante en fin de tableau d'unité du joueur
         */
        public void passerUnite()
        {
            getJoueurCourant().getUnites().Remove(getUniteCourante());
            getJoueurCourant().addUnite(getUniteCourante());
            uniteCourante = getJoueurCourant().getFirstMovementAbleUnit();
        }

        /**
         * Passe le tour d'un joueur.
         * Si tous les joueurs ont joué pendant ce tour, lance un nouveau tour
         */
        public void passerTour()
        {
            joueurCourant++;
            if (joueurCourant >= NOMBRE_JOUEURS)
            {
                joueurCourant = 0;
                nouveauTour();
            }
        }

        /**
         * Lance un nouveau tour de jeu
         */
        public void nouveauTour()
        {
            nbTours++;
            checkFinJeu();
            foreach (Joueur j in joueurs)
            {
                j.nouveauTour();
            }
        }

        /**
         * Rend le nombre de tours restants
         */
        public int getToursRestants()
        {
            return nbTourMax - nbTours;
        }

        /**
         * Rend le joueur courant
         */
        public static Joueur getJoueurCourant(){
            return joueurs[joueurCourant];
        }

        /**
         * Rend l'unité courante
         */
        public static Unite getUniteCourante(){
            return getJoueurCourant().getUnites()[uniteCourante];
        }

        /**
         * Regarde si la fin du jeu est arrivée
         * Le cas échéant, lance une action ?
         * TODO: lancer l'action
         */
        bool checkFinJeu()
        {
            if (nbTourMax == nbTours) return true;
            foreach (Joueur j in joueurs)
            {
                if (j.getUnites().Count == 0) return true;
            }

            return false;

        }
    }
}
