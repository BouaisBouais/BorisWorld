using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Small_World;


namespace UnitTests
{
    [TestClass]
    public unsafe class UnitTest1
    {
        FabriqueUnite f;
        int[][] map;
        int** tab2;


        Carte c;

        [TestInitialize]
        public unsafe void Initiliser()
        {
           

           map[0] = new int[3] { (int) TypeCases.MONTAGNE, (int) TypeCases.DESERT, (int) TypeCases.PLAINE };
           map[1] = new int[3] { (int) TypeCases.DESERT, (int) TypeCases.PLAINE, (int) TypeCases.FORET};
           map[2] = new int[3] { (int) TypeCases.EAU, (int) TypeCases.DESERT,(int) TypeCases.MONTAGNE};       

           //c = new Carte(3, map);
        }


        [TestMethod]
        public void TestDeplacementNain()
        {
            f = new FabriqueNain();
            Unite u = f.fabriquerUnite(1, new Coordonnee(1, 1));


            
        }
    }
}
