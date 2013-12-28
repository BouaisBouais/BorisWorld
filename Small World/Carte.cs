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

       
        static public Case getCase(Coordonnee coord)
        {
            int type = grid[coord.getX()-1][coord.getY()-1];

            switch (type)
            {
                case (int)typeCases.DESERT:
                    return FabriqueCase.obtenirCase(typeCases.DESERT);
                case (int)typeCases.EAU:
                    return FabriqueCase.obtenirCase(typeCases.EAU);
                case (int)typeCases.FORET:
                    return FabriqueCase.obtenirCase(typeCases.FORET);
                case (int)typeCases.MONTAGNE:
                    return FabriqueCase.obtenirCase(typeCases.MONTAGNE);
                case (int)typeCases.PLAINE:
                default:
                    return FabriqueCase.obtenirCase(typeCases.PLAINE);
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
