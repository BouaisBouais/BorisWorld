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
        private bool inGame;


        public ChargerSauver()
        {
            InitializeComponent();
        }

        public ChargerSauver(bool c, bool ingame)
        {
            InitializeComponent();
            charger = c;
            inGame = ingame;
            if (charger)
            {
                header.Text = "Choisissez le fichier à charger :";
                Title = "Charger une partie";
            }
            else
            {
                header.Text = "Choisissez le fichier de sauvegarde :";
                Title = "Sauvegarder une partie";
            }

            listerFichiers();
        }

        private void listerFichiers()
        {

            if (!charger)
            {
                Button newFile = new Button();
                newFile.Content = "Nouvelle Sauvegarde";
                newFile.Style = this.FindResource("SimpleButton") as Style;
                newFile.Click += newsave_Click;

                listeFichiers.Items.Add(newFile);
            }


            List<String> fichiers = new List<String>();
            fichiers = GestionFichiers.getFichiers();

            foreach (String fichier in fichiers)
            {
                Button buttonFichier = new Button();
                buttonFichier.Content = fichier;
                buttonFichier.Name = "button" + fichier;
                buttonFichier.Style = this.FindResource("SimpleButton") as Style;
                buttonFichier.Click += fichierSauvegarde_Click;


                listeFichiers.Items.Add(buttonFichier);
            }
        }

        private void clickAnnuler(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherMenu(inGame);
        }

        private void newsave_Click(object sender, RoutedEventArgs e)
        {

            string nomSession = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            nomSession = nomSession.Split('\\')[1];
            string date = DateTime.Now.ToFileTime().ToString();

            string nom_defaut = nomSession + "_" + date;

            CustomInputBox dialog = new CustomInputBox("Entrez un nom pour la sauvegarde :", nom_defaut);
            dialog.Owner = mainWindow;
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
            dialog.Owner = mainWindow;
            dialog.ShowDialog();
            
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                doAction(nomSauvegarde);


        }

        private void doAction(string n)
        {
            GestionFichiers.action(n, charger);
            mainWindow.afficherJeu();
        }

        private void refreshListeFichiers()
        {
            listeFichiers.Items.Clear();
            listerFichiers();
        }
    }
}