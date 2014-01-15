using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public enum TypeUnite {Viking,Gaulois,Nain};

    [Serializable]
    public class Joueur
    {
        public TypeUnite Peuple { get; set; }
        public int idJoueur {get; set;}
        public int points {get; set;}

        private List<Unite> unites = new List<Unite>();
         


        public Joueur(TypeUnite t, int id)
        {
            Peuple = t;
            idJoueur = id;
            points = 0;
        }

        public void addUnite(Unite unite)
        {
            unites.Add(unite);
        }

        public List<Unite> getUnites()
        {
            return unites;
        }

        /**
         * Retourne le nombre total de points du joueur
         */
        public void calculerPoints()
        {
            int total = 0;
            List<Coordonnee> dejaCompte = new List<Coordonnee>();

            foreach (Unite u in unites)
            {
               
                if (!dejaCompte.Contains(u.coordonnees))
                {
                    total += u.getPoints();
                    dejaCompte.Add(u.coordonnees);
                }
            }
            points += total;
        }

        /**
         * Mets à jour les unités pour un nouveau tour
         */
        public void nouveauTour()
        {
            foreach (Unite u in unites)
            {
                u.mouvement = Unite.MOUVEMENT_MAX;
            }
        }

        /**
         * Retourne l'index de la première unité pouvant se déplacer.
         * Si aucune unité ne peut se déplacer, retourne -1
         */
        public int getFirstMovementAbleUnit()
        {
            int index = -1;
            for (int i = 0; i < unites.Count; i++)
            {
                if (unites[i].peutSeDeplacer())
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
