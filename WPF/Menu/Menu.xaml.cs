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
