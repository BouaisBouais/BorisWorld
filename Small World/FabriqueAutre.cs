using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public enum TypeCarte{Demo, Petite, Normale};

    public class FabriqueAutre
    {
        public Carte creerCarte(TypeCarte type)
        {
            switch (type)
            {
                case TypeCarte.Demo:
                    return new Carte(5);
                case TypeCarte.Petite:
                    return new Carte(10);
                case TypeCarte.Normale:
                default:
                    return new Carte(15);
            }
        }

        Joueur creerJoueur(TypeUnite peuple)
        {
            return new Joueur(peuple);
        }
    }
}
