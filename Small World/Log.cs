using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_World
{
    /* 
     * Stocke les informations relatives au déroulement d'un combat
     */
    public class Log
    {
        public TypeUnite typeAtk {get;set;} // Race de l'attaquant
        public TypeUnite typeDef {get;set;}// Race défenseur

        public int nbCombat {get;set;}// Nombre de combats
        public List<string> logCombat {get;set;} // Log des combats

        public resultatCombat result {get;set;}// resultatdes combat

        public bool logVide {get; set;} // Indique si le log est rempli ou non

        public Log()
        {
            logVide = true;
            logCombat = new List<string>();
        }
        /*
        public void remplirLog (TypeUnite atk, TypeUnite def, int nb, List<string> log, resultatCombat r)
        {
            typeAtk = atk;
            typeDef = def;
            nbCombat = nb;
            logCombat = log;
            result = r;
            logVide = false;
        }*/
    }
}
