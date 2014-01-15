using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class CaseForet : Case
    {
        static private Uri uri = new Uri(@"Ressources/terrains/forest.gif", UriKind.RelativeOrAbsolute);
        static public Uri getUri()
        {
            return uri;
        }
        public TypeCases getTypeCase()
        {
            return TypeCases.FORET;
        }
    }
}
