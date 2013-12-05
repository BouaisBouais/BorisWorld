using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_World
{
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
            if (x > tailleMax) {
                x = tailleMax;
            }
            else if (x < 1)
            {
                x = 1;
            }
            else if (y > tailleMax)
            {
                y = tailleMax;
            }
            else if (y < 1)
            {
                y = 1;
            }

            X = x;
            Y = y;
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
            X = x;
        }

        public int getY()
        {
            return Y;
        }

        public void setY(int y)
        {
            Y = y;
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
