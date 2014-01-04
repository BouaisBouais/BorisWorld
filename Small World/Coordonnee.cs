using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_World
{
    [Serializable]
    public class Coordonnee
    {
        private int X;
        private int Y;
        static public int tailleMax = 0;

        public Coordonnee()
        {
            X = 1;
            Y = 1;
        }

        public Coordonnee(int x, int y)
        {
            X = (x < 1) ? 1 : ((x > tailleMax) ? tailleMax : x);
            Y = (y < 1) ? 1 : ((y > tailleMax) ? tailleMax : y);
        }

        unsafe public Coordonnee(int* array)
        {
	        int x = array[0];
	        int y = array[1];
            X = (x < 1) ? 1 : ((x > tailleMax) ? tailleMax : x);
            Y = (y < 1) ? 1 : ((y > tailleMax) ? tailleMax : y);
        }

        static public Coordonnee get(int x, int y)
        {
            return new Coordonnee(x, y);
        }

        static public void initialiser(int tailleMaximale)
        {
            tailleMax = tailleMaximale;
        }

        public int getX()
        {
            return X;
        }

        public void setX(int x)
        {
            X = (x < 1) ? 1 : ((x > tailleMax) ? tailleMax : x);
        }

        public int getY()
        {
            return Y;
        }

        public void setY(int y)
        {
            Y = (y < 1) ? 1 : ((y > tailleMax) ? tailleMax : y);
        }

        public void clone(Coordonnee coords)
        {
            X = coords.getX();
            Y = coords.getY();
        }

        public Coordonnee decaler(int x, int y)
        {
            return new Coordonnee(X + x, Y + y);
        }

        public int distance(Coordonnee coords)
        {
            int newX = Math.Abs(coords.X - X);
            int newY = Math.Abs(coords.Y - Y);

            return newX + newY;
        }
    }
}
