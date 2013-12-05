using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class CaseDesert : Case
    {
        public void print()
        {
            Console.Write("Case");
        }

        public typeCases getTypeCase()
        {
            return typeCases.DESERT;
        }
    }
}
