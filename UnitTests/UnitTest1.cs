using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Small_World;


namespace UnitTests
{
    [TestClass]
    public unsafe class UnitTest1
    {
        FabriqueUnite f;
        Coordonnee c;
        Unite u;
        int[,] grid;
        Carte carte;

        [TestInitialize]
        public unsafe void Initiliser()
        {
            SmallWorld.Instance.nouvellePartie(TypeCarte.DEMO, TypeUnite.Gaulois, TypeUnite.Nain);
            SmallWorld.Instance.joueurs[0].getUnites().Clear();
            SmallWorld.Instance.joueurs[1].getUnites().Clear();

            grid = new int[3, 3];
            grid[0, 0] = (int)TypeCases.MONTAGNE;
            grid[1, 0] = (int)TypeCases.DESERT;
            grid[2, 0] = (int)TypeCases.PLAINE;
            grid[0, 1] = (int)TypeCases.DESERT;
            grid[1, 1] = (int)TypeCases.PLAINE;
            grid[2, 1] = (int)TypeCases.FORET;
            grid[0, 2] = (int)TypeCases.EAU;
            grid[1, 2] = (int)TypeCases.DESERT;
            grid[2, 2] = (int)TypeCases.MONTAGNE;
            carte = new Carte(3, grid);
        }

        [TestMethod]
        public void TestPoints()
        {
            //Gaulois
            f = new FabriqueGaulois();
            // - Montagne
            c = new Coordonnee(1, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 0);
            // - Plaine
            c = new Coordonnee(3, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 2);
            // - Desert
            c = new Coordonnee(2, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 1);
            // - Foret
            c = new Coordonnee(3, 2);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 1);

            //Nain
            f = new FabriqueNain();
            // - Montagne
            c = new Coordonnee(1, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 1);
            // - Plaine
            c = new Coordonnee(3, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 0);
            // - Desert
            c = new Coordonnee(2, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 1);
            // - Foret
            c = new Coordonnee(3, 2);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 2);

            //Vinking
            f = new FabriqueViking();
            // - Montagne
            c = new Coordonnee(1, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 1);
            // - Plaine
            c = new Coordonnee(3, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 1);
            // - Desert
            c = new Coordonnee(2, 1);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 0);
            // - Foret
            c = new Coordonnee(3, 2);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 1);
            // - Eau
            c = new Coordonnee(1, 3);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 0);
            // - Bord Eau
            c = new Coordonnee(1, 2);
            u = f.fabriquerUnite(c);
            Assert.AreEqual(u.getPoints(), 2);
        }


        [TestMethod]
        public void TestDeplacementGaulois()
        {
            Coordonnee destination;

            f = new FabriqueGaulois();
            c = new Coordonnee(1, 2);
            // - Vers case basique
            destination = new Coordonnee(1, 1); //Montagne
            u = f.fabriquerUnite(c);
            u.deplacement(destination);
            Assert.AreEqual(u.mouvement, 0);
            Assert.IsTrue(u.coordonnees.Equals(destination));
            // - Vers case plaine
            destination = new Coordonnee(2, 2); //Plaine
            u = f.fabriquerUnite(c);
            u.deplacement(destination);
            Assert.AreEqual(u.mouvement, 0.5);
            Assert.IsTrue(u.coordonnees.Equals(destination));
            // - Vers case eau
            destination = new Coordonnee(1, 3); //Eau
            u = f.fabriquerUnite(c);
            u.deplacement(destination);
            Assert.AreEqual(u.mouvement, 1);
            Assert.IsTrue(u.coordonnees.Equals(c));
        }


        [TestMethod]
        public void TestDeplacementNain()
        {
            Coordonnee destination;

            f = new FabriqueNain();
            c = new Coordonnee(1, 2); //Desert
            // - Vers case basique
            destination = new Coordonnee(1, 1); //Montagne
            u = f.fabriquerUnite(c);
            u.deplacement(destination);
            Assert.AreEqual(u.mouvement, 0);
            Assert.IsTrue(u.coordonnees.Equals(destination));
            // - Vers case eau
            destination = new Coordonnee(1, 3); //Eau
            u = f.fabriquerUnite(c);
            u.deplacement(destination);
            Assert.AreEqual(u.mouvement, 1);
            Assert.IsTrue(u.coordonnees.Equals(c));
            // - Montagne -> Montagne
            c = new Coordonnee(1, 1); //Montagne
            destination = new Coordonnee(3, 3); //Montagne
            u = f.fabriquerUnite(c);
            u.deplacement(destination);
            Assert.AreEqual(u.mouvement, 0);
            Assert.IsTrue(u.coordonnees.Equals(destination));
        }


        [TestMethod]
        public void TestDeplacementViking()
        {
            Coordonnee destination;

            f = new FabriqueViking();
            c = new Coordonnee(1, 2); //Desert
            // - Vers case basique
            destination = new Coordonnee(1, 1); //Montagne
            u = f.fabriquerUnite(c);
            u.deplacement(destination);
            Assert.AreEqual(u.mouvement, 0);
            Assert.IsTrue(u.coordonnees.Equals(destination));
            // - Vers case eau
            destination = new Coordonnee(1, 3); //Eau
            u = f.fabriquerUnite(c);
            u.deplacement(destination);
            Assert.AreEqual(u.mouvement, 0);
            Assert.IsTrue(u.coordonnees.Equals(destination));
        }


        //To Run LAST (modify the map)
        [TestMethod]
        public void TestSauvegardeChargement()
        {
            SmallWorld.Instance.nouvellePartie(TypeCarte.DEMO, TypeUnite.Viking, TypeUnite.Nain);
            Unite testUnite = SmallWorld.Instance.getUniteCourante();
            Case testCase = SmallWorld.Instance.carte.getCase(new Coordonnee(1, 1));
            int testNbTours = SmallWorld.Instance.nbTours;

            //On sauvegarde
            GestionFichiers.action("UnitTest", false);

            //On modifie le jeu
            SmallWorld.Instance.getUniteCourante().deplacement(new Coordonnee(1, 2));
            SmallWorld.Instance.passerTour();

            //On charge
            GestionFichiers.action("UnitTest", true);

            //On vérifie
            Assert.IsTrue(testUnite.coordonnees.Equals(SmallWorld.Instance.getUniteCourante().coordonnees));
            Assert.AreEqual(testCase, SmallWorld.Instance.carte.getCase(new Coordonnee(1, 1)));
            Assert.AreEqual(testNbTours, SmallWorld.Instance.nbTours);
        }
    }
}
