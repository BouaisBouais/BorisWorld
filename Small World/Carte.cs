using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{

    unsafe public class Carte
    {
        static int** grid;
        static int taille;
        WrapperMap wrapper;

        public List<Coordonnee> departJoueurs { get; private set; }
        


        unsafe public Carte(int taille)
        {
            Coordonnee.initialiser(taille);
            wrapper = new WrapperMap();
            grid = wrapper.genererMap(taille);
            Carte.taille = taille;

            int** depart = wrapper.posJoueurs();
            for (int i = 0; i < SmallWorld.NOMBRE_JOUEURS; i++)
            {
                departJoueurs.Add(new Coordonnee(depart[i]));
            }
        }

        unsafe public Carte(int taille, int** g)
        {
            grid = g;
            Carte.taille = taille;
        }

        static public int getTaille()
        {
            return taille;
        }

        // Renvoie le nombre d'unité de la carte en fonction de sa taille
        static public int getNombreUniteMax()
        {
            switch (taille)
            {
                case (int) TypeCarte.DEMO:
                    return 4;
                case (int) TypeCarte.PETITE:
                    return 6;
                case (int) TypeCarte.NORMALE:
                    return 8;
                default:
                    throw new Exception("Taille de la carte non reconnue");
            }
        }


        // Renvoie le nombre de tour de la carte en fonction de sa taille
        static public int getNombreTours()
        {
            switch (taille)
            {
                case (int)TypeCarte.DEMO:
                    return 5;
                case (int)TypeCarte.PETITE:
                    return 20;
                case (int)TypeCarte.NORMALE:
                    return 30;
                default:
                    throw new Exception("Taille de la carte non reconnue");
            }
        }


        static public bool bordEau(Coordonnee coords){
            return ((coords.getX() > 1 && getCase(coords.decaler(-1, 0)).getTypeCase() == TypeCases.EAU) ||
            (coords.getY() > 1 && getCase(coords.decaler(0, -1)).getTypeCase() == TypeCases.EAU) ||
            (coords.getX() < taille && getCase(coords.decaler(1, 0)).getTypeCase() == TypeCases.EAU) ||
            (coords.getY() < taille && getCase(coords.decaler(0, 1)).getTypeCase() == TypeCases.EAU));
        }

       
        static public Case getCase(Coordonnee coord)
        {
            int type = grid[coord.getX()-1][coord.getY()-1];

            switch (type)
            {
                case (int)TypeCases.DESERT:
                    return FabriqueCase.obtenirCase(TypeCases.DESERT);
                case (int)TypeCases.EAU:
                    return FabriqueCase.obtenirCase(TypeCases.EAU);
                case (int)TypeCases.FORET:
                    return FabriqueCase.obtenirCase(TypeCases.FORET);
                case (int)TypeCases.MONTAGNE:
                    return FabriqueCase.obtenirCase(TypeCases.MONTAGNE);
                case (int)TypeCases.PLAINE:
                default:
                    return FabriqueCase.obtenirCase(TypeCases.PLAINE);
            }

        }

        unsafe public void print()
        {
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }

           for (int i = 0; i < SmallWorld.NOMBRE_JOUEURS; i++) {
                Console.WriteLine("Depart J"+ i + " : (" + departJoueurs[i].getX() + "," + departJoueurs[i].getY() + ")");
            }
        }

    }
}
