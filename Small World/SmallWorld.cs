using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{

    public class SmallWorld
    {
        static void Main(string[] args)
        {
            FabriqueAutre fabrique = new FabriqueAutre();
            Carte carte = fabrique.creerCarte(TypeCarte.Normale);
            carte.print();

            Case tile = Carte.getCase(1,1);
            tile.print();
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
