using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class MonteurPartie
    {
        private FabriqueAutre fabAutre;
        private FabriqueUnite fabUnit;

        public Carte carteJeu { get; private set; }
        public List<Joueur> joueurs { get; private set; }

        public MonteurPartie()
        {
            fabAutre = new FabriqueAutre();
        }

        public void creerPartie()
        {
            // TODO : A récupérer
            TypeCarte t = TypeCarte.DEMO;

            carteJeu = fabAutre.creerCarte(t);
   
            // TODO : A récupérer
            TypeUnite typeJ1 = TypeUnite.Nain;
            TypeUnite typeJ2 = TypeUnite.Viking;

            joueurs.Add(fabAutre.creerJoueur(typeJ1,0));
            joueurs.Add(fabAutre.creerJoueur(typeJ2,1));

            SmallWorld.joueurs = joueurs;

            int nombreUnites = Carte.getNombreUniteMax();
            List<Coordonnee> posDepartJoueurs = carteJeu.departJoueurs;
            foreach(Joueur j in SmallWorld.joueurs)
            {

                switch (j.Peuple)
                {
                    case TypeUnite.Gaulois:
                        fabUnit = new FabriqueGaulois();
                        break;
                    case TypeUnite.Viking:
                        fabUnit = new FabriqueViking();
                        break;
                    case TypeUnite.Nain:
                        fabUnit = new FabriqueNain();
                        break;
                    default:
                        throw new Exception("Type unité non reconnue");
                }


                for (int i = 0; i < nombreUnites; i++)
                {
                    j.addUnite(fabUnit.fabriquerUnite(posDepartJoueurs[j.idJoueur])); 
                    
                }
            }
            
        }

    }
}
