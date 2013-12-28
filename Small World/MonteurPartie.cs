using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class MonteurPartie
    {
        FabriqueAutre fabAutre;
        FabriqueUnite fabUnit;
        Carte carte;
        Joueur j1;
        Joueur j2;

        public MonteurPartie()
        {
            fabAutre = new FabriqueAutre();

            TypeCarte t = TypeCarte.Demo; // TODO : A récupérer

            carte = fabAutre.creerCarte(t);



        }


    }
}
