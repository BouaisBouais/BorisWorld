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

namespace WPF.Jeu
{
    /// <summary>
    /// Interaction logic for Jeu1.xaml
    /// </summary>
    public partial class Jeu : Page
    {
        private MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public Jeu()
        {
            InitializeComponent();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            actualiserDonnées();

            tourJ1.Foreground = Brushes.Red;
            tourJ2.Foreground = Brushes.Red;

            TypeUnite j1 = SmallWorld.joueurs[0].Peuple;
            switch (j1)
            {
                case TypeUnite.Gaulois:
                    peupleJ1.Foreground = Brushes.Yellow;
                    peupleJ1.Text = "Gaulois";
                    break;
                case TypeUnite.Nain:
                    peupleJ1.Foreground = Brushes.SlateGray;
                    peupleJ1.Text = "Nain";
                    break;
                case TypeUnite.Viking:
                    peupleJ1.Foreground = Brushes.Purple;
                    peupleJ1.Text = "Viking";
                    break;
            }
            TypeUnite j2 = SmallWorld.joueurs[1].Peuple;
            switch (j2)
            {
                case TypeUnite.Gaulois:
                    peupleJ2.Foreground = Brushes.Yellow;
                    peupleJ2.Text = "Gaulois";
                    break;
                case TypeUnite.Nain:
                    peupleJ2.Foreground = Brushes.SlateGray;
                    peupleJ2.Text = "Nain";
                    break;
                case TypeUnite.Viking:
                    peupleJ2.Foreground = Brushes.Purple;
                    peupleJ2.Text = "Viking";
                    break;
            }
        }

        public void actualiserDonnées()
        {
            nbToursRestants.Text = App.gameEngine.getToursRestants().ToString();
            ptsJ1.Text = SmallWorld.joueurs[0].getPoints().ToString();
            ptsJ2.Text = SmallWorld.joueurs[1].getPoints().ToString();
            unitesJ1.Text = SmallWorld.joueurs[0].getUnites().Count.ToString();
            unitesJ2.Text = SmallWorld.joueurs[1].getUnites().Count.ToString();

            if (SmallWorld.joueurCourant == 0)
            {
                tourJ1.Text = "A ton tour !";
                tourJ2.Text = "";
            }
            else
            {
                tourJ2.Text = "A ton tour !";
                tourJ1.Text = "";
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                mainWindow.afficherMenu(true);
        }
    }
}