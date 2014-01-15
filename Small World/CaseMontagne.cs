﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class CaseMontagne : Case
    {
        static private Uri uri = new Uri(@"Ressources/terrains/montagne.png", UriKind.RelativeOrAbsolute);
        static public Uri getUri()
        {
            return uri;
        }
        public TypeCases getTypeCase()
        {
            return TypeCases.MONTAGNE;
        }
    }
}
