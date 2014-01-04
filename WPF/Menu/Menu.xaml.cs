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

namespace WPF.Menu
{
    /// <summary>
    /// Interaction logic for Menu1.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
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
                retour.Content = "Retour à la partie";
                retour.Margin = new Thickness(30);
                retour.Click += clickRetour;

                Grid.SetRow(retour, 0);
                gridMain.Children.Add(retour);
            }
        }

        private void clickRetour(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherJeu();
        }

        private void clickNouvellePartie(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherNouvellePartie();
        }

        private void clickSauvegarder(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherChargerSauver(false);
        }

        private void clickCharger(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherChargerSauver(true);
        }

        private void clickQuitter(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
