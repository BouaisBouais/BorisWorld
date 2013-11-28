using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Nain : Unite
    {
        public Nain(int id, Coordonnee coords) : base (id, coords)
        {
        }

        public override void getPoints() { }

        public override void deplacement(Case c, Coordonnee coords)
        {
            if (c is CaseMontagne) //TODO: Il faut vérifier que le Nain est bien sur une montagne aussi
            {
                mouvement -= 0.5;
                doDeplacement(coords);

            }
            else if (!(c is CaseEau) && mouvement >= 1)
            {
                mouvement--;
                doDeplacement(coords);
            }
        }

    }
}
