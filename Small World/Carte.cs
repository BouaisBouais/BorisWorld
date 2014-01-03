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
        Coordonnee departJ1;
        Coordonnee departJ2;

        unsafe public Carte(int taille)
        {
            Coordonnee.initialiser(taille);
            wrapper = new WrapperMap();
            grid = wrapper.genererMap(taille);
            Carte.taille = taille;

            int** depart = wrapper.posJoueurs();
            departJ1 = new Coordonnee(depart[0]);
            departJ2 = new Coordonnee(depart[1]);
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
            Console.WriteLine("Depart J1 : (" + departJ1.getX() + "," + departJ1.getY() + ")");
            Console.WriteLine("Depart J2 : (" + departJ2.getX() + "," + departJ2.getY() + ")");
        }
    }
}
