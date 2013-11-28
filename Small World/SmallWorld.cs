using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{

    public class SmallWorld
    {
        static void Main(string[] args)
        {
            WrapperMap wrapper = new WrapperMap();

            unsafe
            {
                int** map = wrapper.genererMap(5);
                	for(int x=0; x<5; x++){
		                for(int y=0; y<5; y++){
                            Console.WriteLine(map[x][y]);
                        }
                        Console.WriteLine("\n");
                    }

            }

            
        }



        void nouvellePartie()
        {
        }

        void sauvegarder()
        {
        }

        void charger()
        {
        }

        void checkFinJeu()
        {
        }
    }
}
