using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Small_World
{
    public class GestionFichiers
    {
        public static List<String> getFichiers()
        {
            List<String> fichiers = new List<String>();

            string path = @"Sauvegardes";

            try 
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path)) 
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            } 
            catch (Exception e) 
            {
                Console.WriteLine("Erreur creation dossier sauvegarde: {0}", e.ToString());
            } 

            string[] files = Directory.GetFiles(path);
            foreach (String file in files)
            {
                if (Path.GetExtension(file) == ".lol")
                {
                    fichiers.Add(Path.GetFileNameWithoutExtension(file));
                }
            }

            return fichiers;
        }
    }
}
