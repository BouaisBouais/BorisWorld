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
using WPF.InputBox;
using WPF.OkCancelDialog;


namespace WPF.ChargerSauver
{
    /// <summary>
    /// Interaction logic for ChargerSauver1.xaml
    /// </summary>
    public partial class ChargerSauver : Page
    {
        private MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        private bool charger;


        public ChargerSauver()
        {
            InitializeComponent();
        }

        public ChargerSauver(bool c)
        {
            InitializeComponent();
            charger = c;
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
            }

            listerFichiers();
        }

        private void listerFichiers()
        {

            if (!charger)
            {
                Button newFile = new Button();
                newFile.Content = "Nouvelle Sauvegarde";
                newFile.Height = 100;
                newFile.Click += newsave_Click;

                listeFichiers.Items.Add(newFile);
            }


            List<String> fichiers = new List<String>();
            fichiers = GestionFichiers.getFichiers();

            foreach (String fichier in fichiers)
            {
                Button buttonFichier = new Button();
                buttonFichier.Content = fichier;
                buttonFichier.Height = 100;
                buttonFichier.Name = "button" + fichier;

                buttonFichier.Click += fichierSauvegarde_Click;


                listeFichiers.Items.Add(buttonFichier);
            }
        }



        private void clickAnnuler(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherMenu(false);
        }

        private void newsave_Click(object sender, RoutedEventArgs e)
        {

            string nomSession = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            nomSession = nomSession.Split('\\')[1];
            string date = DateTime.Now.ToFileTime().ToString();

            string nom_defaut = nomSession + "_" + date;

            CustomInputBox dialog = new CustomInputBox("Entrez un nom pour la sauvegarde :", nom_defaut);
            dialog.ShowDialog();
            if (!dialog.Canceled)
                doAction(dialog.InputText);


        }


        private void fichierSauvegarde_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string nomSauvegarde = b.Content as String;

            string message;
            if (charger)
            {
                message = "Charger la partie " + nomSauvegarde + " ?";
            }
            else
            {
                message = "Remplacer la sauvegarde " + nomSauvegarde + " ?";
            }

            OKCancelDialog dialog = new OKCancelDialog(message);
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                doAction(nomSauvegarde);


        }



        private void doAction(string n)
        {
            GestionFichiers.action(n, charger);

            if (charger)
            {
                mainWindow.afficherJeu();
            }
            else
            {
                refreshListeFichiers();
            }
        }




        private void refreshListeFichiers()
        {
            listeFichiers.Items.Clear();
            listerFichiers();
        }




        private void faireAction_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}