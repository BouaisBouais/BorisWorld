using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{

    [Serializable]
    public class Carte
    {
        public int[,] grid { get; set; }
        public int taille { get; set; }
        public String chaineGeneration { get; set; }
        [NonSerialized] public static WrapperMap wrapper = new WrapperMap();
        public List<Coordonnee> vortexs { get; set; }

        private List<Coordonnee> departJoueurs;

        unsafe public Carte(int t)
        {
            vortexs = new List<Coordonnee>();
            this.taille = t;

            chaineGeneration = RandomString(t);
            Console.WriteLine(chaineGeneration);

            int** gridTemp = wrapper.genererMap(t, chaineGeneration);

            grid = new int[t, t];
            for (int i = 0; i < t; i++)
            {
                for (int j = 0; j < t; j++)
                {
                    int typeCase = gridTemp[i][j];
                    grid[i, j] = typeCase;
                }
            }

            departJoueurs = new List<Coordonnee>();
        }

        unsafe public void load()
        {
            wrapper.genererMap(taille, chaineGeneration);
        }

        public Carte(int t, int[,] g)
        {
            grid = g;
            this.taille = t;
        }

        public void initialiseVortexs()
        {
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    int typeCase = grid[i, j];
                    if (typeCase == (int)TypeCases.VORTEX)
                    {
                        Coordonnee c = new Coordonnee(i + 1, j + 1);
                        vortexs.Add(c);
                    }
                }
            }
        }

        public List<Coordonnee> getDepartJoueurs()
        {
            // On le crée quand on le demande(permet le temps à coordonnee de récupérer la taille de la carte)
            if (departJoueurs.Count == 0)
            {
                unsafe
                {
                    int** depart = wrapper.posJoueurs();
                    for (int i = 0; i < SmallWorld.NOMBRE_JOUEURS; i++)
                    {
                        int x = depart[i][0];
                        int y = depart[i][1];
                        Coordonnee coords = new Coordonnee(x, y);
                        departJoueurs.Add(coords);
                    }
                }
            }
            return departJoueurs;
        }


        // Renvoie le nombre d'unité de la carte en fonction de sa taille
        public int getNombreUniteMax()
        {
            switch (taille)
            {
                case (int)TypeCarte.DEMO:
                    return 4;
                case (int)TypeCarte.PETITE:
                    return 6;
                case (int)TypeCarte.NORMALE:
                    return 8;
                default:
                    throw new Exception("Taille de la carte non reconnue");
            }
        }


        // Renvoie le nombre de tour de la carte en fonction de sa taille
        public int getNombreTours()
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


        public bool bordEau(Coordonnee coords)
        {
            return ((coords.getX() > 1 && getCase(coords.decaler(-1, 0)).getTypeCase() == TypeCases.EAU) ||
            (coords.getY() > 1 && getCase(coords.decaler(0, -1)).getTypeCase() == TypeCases.EAU) ||
            (coords.getX() < taille && getCase(coords.decaler(1, 0)).getTypeCase() == TypeCases.EAU) ||
            (coords.getY() < taille && getCase(coords.decaler(0, 1)).getTypeCase() == TypeCases.EAU));
        }


        public Case getCase(Coordonnee coord)
        {
            int type = grid[coord.getX() - 1, coord.getY() - 1];

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
                case (int)TypeCases.VORTEX:
                    return FabriqueCase.obtenirCase(TypeCases.VORTEX);
                case (int)TypeCases.PLAINE:
                default:
                    return FabriqueCase.obtenirCase(TypeCases.PLAINE);
            }

        }


        unsafe public void print()
        {
            for (int j = 0; j < taille; j++)
            {
                for (int i = 0; i < taille; i++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }

            for (int i = 0; i < SmallWorld.NOMBRE_JOUEURS; i++)
            {
                Console.WriteLine("Depart J" + i + " : (" + getDepartJoueurs()[i].getX() + "," + getDepartJoueurs()[i].getY() + ")");
            }
        }

        public int getNombreUnites(Coordonnee coord){
            int nb = 0;
            foreach (Joueur j in SmallWorld.Instance.joueurs)
            {
                foreach (Unite u in j.getUnites())
                {
                    if(u.coordonnees.Equals(coord))
                        nb++;
                }
            }
            return nb;
        }

        public bool presenceUniteEnnemie(Coordonnee coord, Unite amie)
        {
            foreach (Joueur j in SmallWorld.Instance.joueurs)
            {
                if (j.Peuple != amie.getPeuple())
                {
                    foreach (Unite u in j.getUnites())
                    {
                        if (u.coordonnees.Equals(coord))
                            return true;
                    }
                }
            }
            return false;
        }

        public Dictionary<Unite,int> getUnites(Coordonnee coord)
        {
            Dictionary<Unite, int> listeUnite = new Dictionary<Unite, int>();
            bool joueurTrouve = false; // Optimisation de la boucle


            foreach (Joueur j in SmallWorld.Instance.joueurs)
            {
                int i = 0; // ID dans le tableau des unitées
                foreach (Unite u in j.getUnites())
                {
                    if (u.coordonnees.Equals(coord))
                    {
                        listeUnite.Add(u, i);
                        joueurTrouve = true;
                    }
                    i++;
                }
                if (joueurTrouve)
                    break;
            }
            return listeUnite;
        }

        /**
         * Retourne des suggestions de déplacement pour l'unité courante
         * Return List<Coordonnee> liste de coordonnées de cases suggérées
         */
        unsafe public List<Coordonnee> getSuggestions()
        {
            int x = SmallWorld.Instance.getUniteCourante().coordonnees.getX();
            int y = SmallWorld.Instance.getUniteCourante().coordonnees.getY();

            int** suggestions = wrapper.suggestions((int) SmallWorld.Instance.getJoueurCourant().Peuple, x, y);

            List<Coordonnee> retour = new List<Coordonnee>();

            for (int i = 0; i < 3; i++)//3 suggestions. TODO: Changement automatique en fonction du nombre reçu
            {
                int suggx = suggestions[i][0];
                int suggy = suggestions[i][1];
                if (suggx != 0 && suggy != 0)
                {
                    Coordonnee suggestion = new Coordonnee(suggx, suggy);
                    retour.Add(suggestion);
                }
            }
            return retour;
        }

        /**
         * Bouge (avec probabilité) les unités sur les vortex sur les autres vortex inoccupés
         */
        public void bougeVortex()
        {
            foreach(Joueur j in SmallWorld.Instance.joueurs)
            {
                foreach (Unite u in j.getUnites())
                {
                    if (getCase(u.coordonnees).getTypeCase() == TypeCases.VORTEX)
                    {
                        Coordonnee newPos = vortexs[_rng.Next(vortexs.Count)];
                        if (!presenceUniteEnnemie(newPos, u))
                        {
                            u.coordonnees.setX(newPos.getX());
                            u.coordonnees.setY(newPos.getY());
                        }
                    }
                }
            }
        }

        private readonly Random _rng = new Random();
        private const string _chars = "0123456789";

        private string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

    }
}