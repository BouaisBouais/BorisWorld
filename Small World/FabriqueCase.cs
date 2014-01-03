using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public enum  TypeCases{
		DESERT = 0,
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

        static public Case obtenirCase(TypeCases type)
        {
            switch (type)
            {
                case TypeCases.DESERT:
                    if (desert == null)
                        creerCaseDesert();
                    return desert;
                case TypeCases.PLAINE:
                    if (plaine == null)
                        creerCasePlaine();
                    return plaine;
                case TypeCases.EAU:
                    if (eau == null)
                        creerCaseEau();
                    return eau;
                case TypeCases.MONTAGNE:
                    if (montagne == null)
                        creerCaseMontagne();
                    return montagne;
                case TypeCases.FORET:
                    if (foret == null)
                        creerCaseForet();
                    return foret;
            }
            throw new Exception("Type de case inconnu");
        }

        static void creerCasePlaine()
        {
            plaine = new CasePlaine();
        }

        static void creerCaseEau()
        {
            eau = new CaseEau();
        }

        static void creerCaseForet()
        {
            foret = new CaseForet();
        }

        static void creerCaseMontagne()
        {
            montagne = new CaseMontagne();
        }

        static void creerCaseDesert()
        {
            desert = new CaseDesert();
        }
    }
}
