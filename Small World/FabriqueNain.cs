using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class FabriqueNain : FabriqueUnite
    {
        public Unite fabriquerUnite(Coordonnee coords)
        {
            return new Nain(coords);
        }
    }
}
