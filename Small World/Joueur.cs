using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public enum TypeUnite {Viking,Gaulois,Nain};

    public class Joueur
    {
        private TypeUnite peuple;
        private List<Unite> unites = new List<Unite>();

        public Joueur(TypeUnite t)
        {
            peuple = t;
        }

        public void addUnite(Unite unite)
        {
            unites.Add(unite);
        }

        public List<Unite> getUnites()
        {
            return unites;
        }
   
    }
}
