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

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page menu;
        private Page chargerSauver;
        private Page jeu;
        private Page nouvellePartie;
        private Page finJeu;


        public MainWindow()
        {
            InitializeComponent();
            afficherMenu(false);
        }

        public void afficherMenu(bool ingame)
        {
            if (ingame)
                menu = new Menu.Menu(ingame);
            else
                menu = new Menu.Menu();
            this.Content = menu;
        }

        public void afficherChargerSauver(bool charger, bool ingame)
        {
            chargerSauver = new ChargerSauver.ChargerSauver(charger, ingame);
            this.Content = chargerSauver;
        }

        public void afficherJeu()
        {
            jeu = new Jeu.Jeu();
            this.Content = jeu;
        }

        public void afficherNouvellePartie(bool inGame)
        {
            if (inGame)
                nouvellePartie = new NouvellePartie.NouvellePartie(inGame);
            else
                nouvellePartie = new NouvellePartie.NouvellePartie();
            this.Content = nouvellePartie;
        }

        public void afficherFinJeu()
        {
            finJeu = new FinJeu.FinJeu();
            this.Content = finJeu;
        }
    }
}