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
        //static public int tailleMax = 0;

        private const int coordMini = 1;

        public Coordonnee()
        {
            X = coordMini;
            Y = coordMini;
        }

        private int getTailleMap() {
            if (SmallWorld.Instance.carte == null)
                return coordMini;
            else
                return SmallWorld.Instance.carte.taille;
        }

        public Coordonnee(int x, int y)
        {
            X = (x < coordMini) ? coordMini : ((x > getTailleMap()) ? getTailleMap() : x);
            Y = (y < coordMini) ? coordMini : ((y > getTailleMap()) ? getTailleMap() : y);
        }

        unsafe public Coordonnee(int* array)
        {
	        int x = array[0];
	        int y = array[1];
            X = (x < coordMini) ? coordMini : ((x > getTailleMap()) ? getTailleMap() : x);
            Y = (y < coordMini) ? coordMini : ((y > getTailleMap()) ? getTailleMap() : y);
        }

        static public Coordonnee get(int x, int y)
        {
            return new Coordonnee(x, y);
        }

        /*
        static public void initialiser(int tailleMaximale)
        {
            tailleMax = tailleMaximale;
        }
        */
        public int getX()
        {
            return X;
        }

        public void setX(int x)
        {
            X = (x < coordMini) ? coordMini : ((x > getTailleMap()) ? getTailleMap() : x);
        }

        public int getY()
        {
            return Y;
        }

        public void setY(int y)
        {
            Y = (y < coordMini) ? coordMini : ((y > getTailleMap()) ? getTailleMap() : y);
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

        public override string ToString()
        {
            return "X:" + X + ",Y:" + Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Coordonnee objAsPart = obj as Coordonnee;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);

        }

        public bool Equals(Coordonnee obj)
        {
            return (X == obj.X && Y == obj.Y);
        }



    }
}
