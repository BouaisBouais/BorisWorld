using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Small_World;

namespace UnitTests
{
    [TestClass]
    public unsafe class UnitTest1
    {
        FabriqueUnite f;
        int** map;
        Carte c;

        [TestInitialize]
        public unsafe void Initiliser()
        {
            

           /* map[0] = new int[3] { (int) typeCases.MONTAGNE, (int) typeCases.DESERT, (int) typeCases.PLAINE };
            map[1] = new int[3] { (int) typeCases.DESERT, (int) typeCases.PLAINE, (int) typeCases.FORET};
            map[2] = new int[3] { (int) typeCases.EAU, (int) typeCases.DESERT,(int) typeCases.MONTAGNE};
             */            
                           
           c = new Carte(3, map);
        }


        [TestMethod]
        public void TestDeplacementNain()
        {
            f = new FabriqueNain();
            Unite u = f.fabriquerUnite(1, new Coordonnee(1, 1));


            
        }
    }
}
