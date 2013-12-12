using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public enum resultatCombat
    {
        ATTAQUANT_MORT = 0,
        DEFENSEUR_MORT,
        DEUX_MORTS,
        AUCUN_MORT
    }


    abstract public class Unite
    {
        protected Coordonnee coordonnees = new Coordonnee();
        protected int attaque;
        protected int defense;
        protected int vie;
        protected double mouvement;
        protected int idJoueur;
        protected double prochainMouvement;

        public const int MOUVEMENT_MAX = 1;
        public const int ATTAQUE_MAX = 2;
        public const int VIE_MAX = 2;
        public const int DEFENSE_MAX = 1;


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
       
        public abstract bool deplacementPossible(Case @case, Coordonnee coords);
        public abstract int getPoints();

        public abstract void deplacement(Case @case, Coordonnee coords)
        {
            if (deplacementPossible(@case, coords))
            {
                doDeplacement(coords);
            }
        }

        public void doDeplacement(Coordonnee newCoords)
        {
            mouvement -= prochainMouvement;
            coordonnees.clone(newCoords);
        }

        public void passerTour()
        {
        }

        // Renvoie le résultat du combat
        public resultatCombat combattre(Unite @unite, bool uniteSeule)
        {

            Random r = new Random();
            double rapportForce = 0;
            int nombreTire = 0;


            // choix de l'unité fait avant par monteur partie ou autre (celui qui à accès aux joueurs)
            if (deplacementPossible(Carte.getCase(@unite.coordonnees), @unite.coordonnees))
            {
                if (@unite.defense != 0)
                {
                    int nombreCombats = r.Next(3, Math.Max(this.vie, @unite.vie) + 2);
                    int attaqueAtt;
                    int defenseDef;

                    while (nombreCombats > 0 && this.vie > 0 && @unite.vie > 0)
                    {
                        attaqueAtt = (this.vie / VIE_MAX) * this.attaque;
                        defenseDef = (this.vie / VIE_MAX) * @unite.defense;
                        nombreTire = r.Next(0, 100);


                        if (attaqueAtt == @unite.attaque)
                        {
                            rapportForce = 50;
                        }
                        else
                        {
                            rapportForce = Math.Round(((attaqueAtt / defenseDef) * 0.5 + 0.5) * 100, 0);
                        }


                        if (nombreTire < rapportForce)
                        {
                            @unite.vie--;
                        }
                        else
                        {
                            this.vie--;
                        }
                    }
                }
                else
                {
                    @unite.vie = 0;
                }

                if (uniteSeule && @unite.vie == 0 && this.vie > 0)
                    doDeplacement(@unite.coordonnees);


                if (@unite.vie == 0 && this.vie == 0)
                {
                    return resultatCombat.DEUX_MORTS;
                }
                else if (@unite.vie == 0)
                {
                    return resultatCombat.DEFENSEUR_MORT;
                }
                else if (this.vie == 0)
                {
                    return resultatCombat.ATTAQUANT_MORT;
                }
                else
                {
                    return resultatCombat.AUCUN_MORT;
                }

            }

            return resultatCombat.AUCUN_MORT;
          
        }




    }
}
