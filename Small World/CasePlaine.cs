using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class CasePlaine : Case
    {
        public void print()
        {
            Console.Write("Case");
        }


        public TypeCases getTypeCase()
        {
            return TypeCases.PLAINE;
        }
    }
}
