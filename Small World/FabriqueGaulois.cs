using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class FabriqueGaulois : FabriqueUnite
    {
        public Unite fabriquerUnite(int id, Coordonnee coords)
        {
            return new Gaulois(id, coords);
        }
    }
}
