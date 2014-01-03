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
        static public Joueur[] joueurs;
        static public int joueurCourant;
        //TODO : monteurPartie.

        public SmallWorld()
        {
            FabriqueAutre fabrique = new FabriqueAutre();
            carte = fabrique.creerCarte(TypeCarte.Normale);
            carte.print();

            Case tile = Carte.getCase(new Coordonnee(1, 1));
            tile.print();

        }

        static void Main(string[] args)
        {
            FabriqueAutre fabrique = new FabriqueAutre();
            Carte carte = fabrique.creerCarte(TypeCarte.Normale);
            carte.print();

            //Case tile = Carte.getCase(Coordonnee.get(1,1));
            //tile.print();
            Case tile = Carte.getCase(new Coordonnee(1,1));
            tile.print();
        }

        public static Joueur getJoueurCourant(){
            return joueurs[joueurCourant];
        }


        void nouvellePartie()
        {
        }

        void sauvegarder()
        {
        }

        void charger()
        {
        }

        void checkFinJeu()
        {
        }
    }
}
