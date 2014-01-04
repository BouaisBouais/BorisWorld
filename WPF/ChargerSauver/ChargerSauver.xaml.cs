using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Small_World;

namespace WPF.ChargerSauver
{
    /// <summary>
    /// Interaction logic for ChargerSauver1.xaml
    /// </summary>
    public partial class ChargerSauver : Page
    {
        private MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public ChargerSauver()
        {
            InitializeComponent();
        }

        public ChargerSauver(bool charger)
        {
            InitializeComponent();
            if (charger)
            {
                header.Text = "Choisissez le fichier à charger :";
                Title = "Charger une partie";
                faireAction.Content = "Charger";
            }
            else
            {
                header.Text = "Choisissez le fichier de sauvegarde :";
                Title = "Sauvegarder une partie";
                faireAction.Content = "Sauvegarder";

                Button newFile = new Button();
                newFile.Content = "Nouvelle Sauvegarde";
                newFile.Height = 100;

                listeFichiers.Items.Add(newFile);
            }

            listerFichiers();
        }

        private void listerFichiers()
        {
            List<String> fichiers = new List<String>();
            fichiers = GestionFichiers.getFichiers();

            foreach (String fichier in fichiers)
            {
                Button buttonFichier = new Button();
                buttonFichier.Content = fichier;
                buttonFichier.Height = 100;
                buttonFichier.Name = "button" + fichier;

                listeFichiers.Items.Add(buttonFichier);
            }
        }

        private void clickAnnuler(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherMenu(false);
        }
    }
}
