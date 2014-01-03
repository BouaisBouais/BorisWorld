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
        static public int joueurCourant {get; private set;}
        static public int NOMBRE_JOUEURS = 2;


        private int nbTours = 0;
        private int nbTourMax;

        enum TypeAction { DEPLACEMENT, PASSER_TOUR};

        public void nouvellePartie(TypeCarte tailleCarte, TypeUnite j1, TypeUnite j2)
        {
            MonteurPartie monteur = new MonteurPartie();
            monteur.nouvellePartie(tailleCarte, j1, j2);
        }

        public void boucle()
        {
            nbTourMax = Carte.getNombreTours();
            TypeAction action;

            while (checkFinJeu())
            {

                foreach (Joueur j in joueurs)
                {
                    joueurCourant = j.idJoueur;
                    if (checkFinJeu()) break;

                    foreach (Unite u in j.getUnites())
                    {
                        // TODO : Recuperer l'action
                        action = TypeAction.DEPLACEMENT;

                        switch (action)
                        {
                            case TypeAction.DEPLACEMENT:
                                // TODO : Recupérer les coordonnées
                                Coordonnee c = new Coordonnee(1, 1);
                                u.deplacement(c);
                                goto case TypeAction.PASSER_TOUR;
                            case TypeAction.PASSER_TOUR:
                                j.pointJoueur += u.getPoints();
                                break;
                            default:
                                throw new Exception("Action inconnue");
                        }

                        if (checkFinJeu()) break;
                    }

                }
                nbTours++;
            }
        }

        public SmallWorld()
        {
        }


        public static Joueur getJoueurCourant(){
            return joueurs[joueurCourant];
        }

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
