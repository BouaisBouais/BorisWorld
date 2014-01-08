using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{
    enum TypeAction { DEPLACEMENT, PASSER_TOUR };

    [Serializable]
    public sealed class SmallWorld
    {
        public Carte carte { get; set; }
        public List<Joueur> joueurs {get; set;}
        public int joueurCourant { get; set; }
        public int uniteCourante { get; set; }
        public const int NOMBRE_JOUEURS = 2;

        public int nbTours { get; set; }
        public int nbTourMax { get; set; }

        private static SmallWorld instance = new SmallWorld();
        public static SmallWorld Instance
        {
             get 
             {
                 return instance; 
             }

             set 
             {
                instance = value;
             }
        }

        private SmallWorld()
        {
            nbTours = 0;
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
         * Return true si fin du jeu
         */
        public bool deplacement(Coordonnee c)
        {
            resultatCombat result = getUniteCourante().deplacement(c);
            if (result == resultatCombat.ATTAQUANT_MORT || getUniteCourante().mouvement < 1)
            {
                int nextUnite = getJoueurCourant().getFirstMovementAbleUnit();
                if (nextUnite == -1)
                    passerTour();
                else
                    instance.uniteCourante = nextUnite;
            }
            return checkFinJeu();
        }

        /**
         * Fais passer l'unité courante en fin de tableau d'unité du joueur
         */
        public void passerUnite()
        {
            Unite u = getUniteCourante();
            u.mouvement = 0;
            // TODO : Utile ?
            //getJoueurCourant().getUnites().Remove(u);
            //getJoueurCourant().addUnite(u);

            int id_next = getJoueurCourant().getFirstMovementAbleUnit();

            if (id_next != -1) {
                instance.uniteCourante = id_next;
            } else {
                passerTour();
            }
        }

        /**
         * Passe le tour d'un joueur.
         * Si tous les joueurs ont joué pendant ce tour, lance un nouveau tour
         * Return true si fin du jeu
         */
        public bool passerTour()
        {
            joueurCourant++;
            uniteCourante = 0;
            if (joueurCourant >= NOMBRE_JOUEURS)
            {
                joueurCourant = 0;
                return nouveauTour();
            }
            return false;
        }

        /**
         * Lance un nouveau tour de jeu
         * Return true si fin du jeu
         */
        public bool nouveauTour()
        {
            nbTours++;
            foreach (Joueur j in joueurs)
            {
                j.nouveauTour();
            }
            return checkFinJeu();
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
        public Joueur getJoueurCourant(){
            return joueurs[joueurCourant];
        }

        /**
         * Rend l'unité courante
         */
        public Unite getUniteCourante(){
            return getJoueurCourant().getUnites()[uniteCourante];
        }

        /**
         * Regarde si la fin du jeu est arrivée
         * Return true si fin du jeu
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

        public Joueur getVainqueur()
        {
            Joueur vainqueur = null;

            foreach (Joueur j in joueurs)
            {
                if (vainqueur == null || vainqueur.getPoints() < j.getPoints())
                {
                    vainqueur = j;
                }
            }

            return vainqueur;
        }
    }
}
