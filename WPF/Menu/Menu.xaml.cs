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
using System.Media;


namespace WPF.Menu
{
    /// <summary>
    /// Interaction logic for Menu1.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        //private SoundPlayer sound = new SoundPlayer("Ressources/SmallWorldIntro.mp3");
        private MediaPlayer sound3 = new MediaPlayer();
        private bool inGame = false;
        public Menu()
        {
            InitializeComponent();
            sound3.Open(new Uri(@"Ressources/SmallWorldIntro.mp3", UriKind.RelativeOrAbsolute));
            sound3.Play();

            //sound.PlayLooping();
            sauvegarder.IsEnabled = false;
        }

        public Menu(bool ingame)
        {
            InitializeComponent();
            sauvegarder.IsEnabled = false;
            if (ingame)
            {
                Button retour = new Button();
                retour.Content = "Retour à la partie";
                retour.Margin = new Thickness(30);
                retour.Click += clickRetour;

                Grid.SetRow(retour, 0);
                retour.Style = this.FindResource("SimpleButton") as Style;
                gridMenu.Children.Add(retour);
                
                sauvegarder.IsEnabled = true;
                inGame = true;
            }
            
        }

        private void clickRetour(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherJeu();
        }

        private void clickNouvellePartie(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherNouvellePartie(inGame);
            sound3.Stop();
        }

        private void clickSauvegarder(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherChargerSauver(false, inGame);
        }

        private void clickCharger(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherChargerSauver(true, inGame);
        }

        private void clickQuitter(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}