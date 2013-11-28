using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public enum  typeCases{
		DESERT,
		EAU,
		FORET,
		MONTAGNE,
		PLAINE
	}

    static public class FabriqueCase
    {
        static private CasePlaine plaine = null;
        static private CaseEau eau = null;
        static private CaseForet foret = null;
        static private CaseMontagne montagne = null;
        static private CaseDesert desert = null;

        static public Case obtenirCase(typeCases type)
        {
            switch (type)
            {
                case typeCases.DESERT:
                    if (desert == null)
                        creerCaseDesert();
                    return desert;
                case typeCases.PLAINE:
                    if (plaine == null)
                        creerCasePlaine();
                    return desert;
                case typeCases.EAU:
                    if (eau == null)
                        creerCaseEau();
                    return desert;
                case typeCases.MONTAGNE:
                    if (montagne == null)
                        creerCaseMontagne();
                    return desert;
                case typeCases.FORET:
                    if (foret == null)
                        creerCaseForet();
                    return desert;
            }
            throw new Exception("Type de case inconnu");
        }

        static void creerCasePlaine()
        {
        }

        static void creerCaseEau()
        {
        }

        static void creerCaseForet()
        {
        }

        static void creerCaseMontagne()
        {
        }

        static void creerCaseDesert()
        {
        }
    }
}
