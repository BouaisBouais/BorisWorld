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
using System.Windows.Shapes;
using WPF.NouvellePartie;
using WPF.ChargerSauver;

namespace WPF.Menu
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();

            //TODO: doesn't work
            ImageSource path = new BitmapImage(new Uri(@"Ressources/title.png", UriKind.RelativeOrAbsolute));
            Image img = new Image();
            img.Source = path;
            gridMain.Children.Add(img);
        }

        public Menu(bool ingame)
        {
            InitializeComponent();
            if (ingame)
            {
                Button retour = new Button();
                retour.Content = "Retour";
                retour.Background = Brushes.White;
                retour.Click += clickRetour;

                Grid.SetRow(retour, 0);
                gridMain.Children.Add(retour);
            }
        }

        private void clickRetour(object sender, RoutedEventArgs e)
        {
            //TODO: retour au jeu
        }

        private void clickNouvellePartie(object sender, RoutedEventArgs e)
        {
            NouvellePartie.NouvellePartie fenetre = new NouvellePartie.NouvellePartie();
            fenetre.Show();
            this.Close();
        }

        private void clickSauvegarder(object sender, RoutedEventArgs e)
        {
            ChargerSauver.ChargerSauver fenetre = new ChargerSauver.ChargerSauver(false);
            fenetre.Show();
            this.Close();
        }

        private void clickCharger(object sender, RoutedEventArgs e)
        {
            ChargerSauver.ChargerSauver fenetre = new ChargerSauver.ChargerSauver(true);
            fenetre.Show();
            this.Close();
        }

        private void clickQuitter(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
