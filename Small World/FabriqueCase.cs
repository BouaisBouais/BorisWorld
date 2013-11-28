using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    enum  typeCases{
		DESERT,
		EAU,
		FORET,
		MONTAGNE,
		PLAINE
	}

    public class FabriqueCase
    {
        private CasePlaine plaine = null;
        private CaseEau eau = null;
        private CaseForet foret = null;
        private CaseMontagne montagne = null;
        private CaseDesert desert = null;

        Case obtenirCase(typeCases type)
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

        void creerCasePlaine()
        {
        }

        void creerCaseEau()
        {
        }

        void creerCaseForet()
        {
        }

        void creerCaseMontagne()
        {
        }

        void creerCaseDesert()
        {
        }
    }
}
