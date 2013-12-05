using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class CaseForet : Case
    {
        public void print()
        {
            Console.Write("Case");
        }

        public typeCases getTypeCase()
        {
            return typeCases.FORET;
        }
    }
}
