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
        AUCUN_MORT,
        DEPLACEMENT_BATAILLE,
        DEPLACEMENT_SIMPLE,
        DEPLACEMENT_IMPOSSIBLE
    }

    [Serializable]
    abstract public class Unite
    {
        public Coordonnee coordonnees { get; protected set; }
        public int attaque { get; set; }
        public int defense { get; set; }
        public int vie { get; set; }
        public double mouvement { get; set; }

        public const int MOUVEMENT_MAX = 1;
        public const int ATTAQUE_MAX = 2;
        public const int VIE_MAX = 2;
        public const int DEFENSE_MAX = 1;

        public Unite(Coordonnee coords)
        {
            coordonnees = new Coordonnee();

            attaque = ATTAQUE_MAX;
            defense = DEFENSE_MAX;
            vie = VIE_MAX;
            mouvement = MOUVEMENT_MAX;

            coordonnees.clone(coords);
        }






        /// <summary>
        /// Regarde en fonction de la case si l'unité peut se déplacer ou pas
        /// </summary>

        public abstract resultatCombat deplacement(Coordonnee coords);
        public abstract bool deplacementPossible(Coordonnee coords);

        public abstract int getPoints();


        public void passerTour()
        {
        }

        /**
         * Renvoie le résultat du combat
         * Detruit l'unité ennemie si elle doit être détruite
         */
        public resultatCombat combattre(int indexUnite, bool uniteSeule, Joueur ennemi)
        {
            Unite @unite = ennemi.getUnites()[indexUnite];
            Random r = new Random();
            double rapportForce = 0;
            int nombreTire = 0;

            if (@unite.defense != 0)
            {
                int nombreCombats = r.Next(3, Math.Max(this.vie, @unite.vie) + 2);
                double attaqueAtt;
                double defenseDef;

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
            ennemi.getUnites()[indexUnite] = @unite;
            SmallWorld.Instance.getJoueurCourant().getUnites()[SmallWorld.Instance.uniteCourante] = this;

            if (uniteSeule && @unite.vie == 0 && this.vie > 0)
            {
                ennemi.getUnites().RemoveAt(indexUnite);
                return resultatCombat.DEPLACEMENT_BATAILLE;
            }

            if (@unite.vie == 0 && this.vie == 0)
            {
                ennemi.getUnites().RemoveAt(indexUnite);
                return resultatCombat.DEUX_MORTS;
            }
            else if (@unite.vie == 0)
            {
                ennemi.getUnites().RemoveAt(indexUnite);
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

        /**
         * Vérifie si une ou plusieurs unité ennemies sont présentes sur la case
         * Le cas échéant, lance une attaque contre l'unité ayant le plus de défense
         * Sinon, déplace l'unité voulant se déplacer
         */
        protected resultatCombat verifUniteCase(Coordonnee coords)
        {

            int defMax = 0;
            int choisie = -1;
            Joueur ennemi = null;
            bool uniteSeule = true;
            foreach (Joueur j in SmallWorld.Instance.joueurs)
            {
                if (j != SmallWorld.Instance.getJoueurCourant())
                {
                    for (int i = 0; i < j.getUnites().Count; i++)
                    {
                        Unite u = j.getUnites()[i];
                        if (u.coordonnees.Equals(coords))
                        {
                            if (choisie != -1) uniteSeule = false;
                            if (u.defense > defMax)
                            {
                                choisie = i;
                                defMax = u.defense;
                                ennemi = j;
                            }
                        }
                    }
                }
            }
            if (choisie != -1)
            {
                return combattre(choisie, uniteSeule, ennemi);
            }
            else
            {
                return resultatCombat.DEPLACEMENT_SIMPLE;
            }
        }

        protected void makeResultatCombat(resultatCombat result, Coordonnee coords)
        {
            switch (result)
            {
                case resultatCombat.ATTAQUANT_MORT:
                case resultatCombat.DEUX_MORTS:
                    SmallWorld.Instance.getJoueurCourant().getUnites().Remove(this);
                    break;
                case resultatCombat.DEPLACEMENT_BATAILLE:
                case resultatCombat.DEPLACEMENT_SIMPLE:
                    coordonnees = coords;
                    break;
            }
        }

        public bool peutSeDeplacer()
        {

            if (this.deplacementPossible(this.coordonnees.decaler(1, 0)) ||
                this.deplacementPossible(this.coordonnees.decaler(0, 1)) ||
                this.deplacementPossible(this.coordonnees.decaler(-1, 0)) ||
                this.deplacementPossible(this.coordonnees.decaler(0, -1))
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
