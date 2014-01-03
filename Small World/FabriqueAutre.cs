using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public enum TypeCarte{DEMO=5, PETITE=10, NORMALE=15};

    public class FabriqueAutre
    {
        public Carte creerCarte(TypeCarte type)
        {
<<<<<<< HEAD
            return new Carte((int)type);
=======
            return new Carte((int) type);
>>>>>>> 09e75f41b3a049967864aee8fe7c989e9dcf51e4
        }

        public Joueur creerJoueur(TypeUnite peuple, int id)
        {
            return new Joueur(peuple, id);
        }
    }
}
