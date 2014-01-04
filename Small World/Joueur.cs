using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public enum TypeUnite {Viking,Gaulois,Nain};


    [Serializable]
    public class Joueur
    {
        public TypeUnite Peuple { get; private set; }
        public int idJoueur {get;   private set;}
        public int pointJoueur { get; set; }
        private List<Unite> unites = new List<Unite>();

        public Joueur(TypeUnite t, int id)
        {
            Peuple = t;
            idJoueur = id;
            pointJoueur = 0;
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
