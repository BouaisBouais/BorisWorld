using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class FabriqueAutre
    {
        Carte creerCarte(string Type)
        {
            return new Carte();
        }

        Joueur creerJoueur(string peuple)
        {
            return new Joueur();
        }
    }
}
