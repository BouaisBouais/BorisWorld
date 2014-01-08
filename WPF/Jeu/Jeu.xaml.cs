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

            this.PreviewKeyDown += new KeyEventHandler(HandleKeys);

            actualiserDonnées();

            tourJ1.Foreground = Brushes.Red;
            tourJ2.Foreground = Brushes.Red;

            TypeUnite j1 = SmallWorld.Instance.joueurs[0].Peuple;
            peupleJ1.Foreground = App.getColorFromPeuple(j1);
            peupleJ1.Text = App.getNameFromPeuple(j1);
            TypeUnite j2 = SmallWorld.Instance.joueurs[1].Peuple;
            peupleJ2.Foreground = App.getColorFromPeuple(j2);
            peupleJ2.Text = App.getNameFromPeuple(j2);
        }

        public void actualiserDonnées()
        {
            // TODO : IDée => Gérer ca avec un tableau pas possible ?
            nbToursRestants.Text = SmallWorld.Instance.getToursRestants().ToString();
            ptsJ1.Text = SmallWorld.Instance.joueurs[0].getPoints().ToString();
            ptsJ2.Text = SmallWorld.Instance.joueurs[1].getPoints().ToString();
            unitesJ1.Text = SmallWorld.Instance.joueurs[0].getUnites().Count.ToString();
            unitesJ2.Text = SmallWorld.Instance.joueurs[1].getUnites().Count.ToString();

            if (SmallWorld.Instance.joueurCourant == 0)
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

        private void HandleKeys(object sender, KeyEventArgs e)
        {
            bool finJeu = false;
            if (e.Key == Key.Escape)
                mainWindow.afficherMenu(true);
            if (e.Key == Key.Left)
            {
                Coordonnee left = SmallWorld.Instance.getUniteCourante().coordonnees.decaler(-1, 0);
                finJeu = SmallWorld.Instance.deplacement(left);
                actualiserDonnées();
            }
            if (e.Key == Key.Right)
            {
                Coordonnee right = SmallWorld.Instance.getUniteCourante().coordonnees.decaler(1, 0);
                finJeu = SmallWorld.Instance.deplacement(right);
                actualiserDonnées();
            }
            if (e.Key == Key.Up)
            {
                Coordonnee up = SmallWorld.Instance.getUniteCourante().coordonnees.decaler(0, -1);
                finJeu = SmallWorld.Instance.deplacement(up);
                actualiserDonnées();
            }
            if (e.Key == Key.Down)
            {
                Coordonnee down = SmallWorld.Instance.getUniteCourante().coordonnees.decaler(0, 1);
                finJeu = SmallWorld.Instance.deplacement(down);
                actualiserDonnées();
            }
            if (e.Key == Key.Space)
            {
                SmallWorld.Instance.passerUnite();
                actualiserDonnées();
            }
            if (e.Key == Key.Enter)
            {
                finJeu = SmallWorld.Instance.passerTour();
                actualiserDonnées();
            }
            canvasCarte.InvalidateVisual();
            if (finJeu)
            {
                mainWindow.afficherFinJeu();
            }
        }


        private void popMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherMenu(true);
        }

        private void canvasCarte_MouseLeave_1(object sender, MouseEventArgs e)
        {

        }
    }
}