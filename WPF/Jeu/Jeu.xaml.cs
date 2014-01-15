﻿using System;
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
            TypeUnite j2 = SmallWorld.Instance.joueurs[1].Peuple;


            /*
            
peupleJ1.Foreground = App.getColorFromPeuple(j1);
peupleJ1.Text = App.getNameFromPeuple(j1);
            
peupleJ2.Foreground = App.getColorFromPeuple(j2);
peupleJ2.Text = App.getNameFromPeuple(j2);*/
            imgPeupleJ1.Source = App.getImageFromPeuple(j1);
            imgPeupleJ2.Source = App.getImageFromPeuple(j2);

            this.canvasCarte.setFenetreParent(this);
            
        }

        public void actualiserDonnées()
        {
            Console.WriteLine("loool");

            nbToursRestants.Text = (SmallWorld.Instance.nbTours + 1 ).ToString();
            nbToursMax.Text = SmallWorld.Instance.nbTourMax.ToString();

            /*
            ptsJ1.Text = SmallWorld.Instance.joueurs[0].getPoints().ToString();
            ptsJ2.Text = SmallWorld.Instance.joueurs[1].getPoints().ToString();
            unitesJ1.Text = SmallWorld.Instance.joueurs[0].getUnites().Count.ToString();
            unitesJ2.Text = SmallWorld.Instance.joueurs[1].getUnites().Count.ToString();*/

            pointJ1.Text = SmallWorld.Instance.joueurs[0].getPoints().ToString();
            pointJ2.Text = SmallWorld.Instance.joueurs[1].getPoints().ToString();
            nbUnitJ1.Text = SmallWorld.Instance.joueurs[0].getUnites().Count.ToString();
            nbUnitJ2.Text = SmallWorld.Instance.joueurs[1].getUnites().Count.ToString();


            SolidColorBrush couleurActif =  Brushes.DarkRed;
            SolidColorBrush couleurInactif = Brushes.White;

            if (SmallWorld.Instance.joueurCourant == 0)
            {
                j1Actif.Foreground = couleurActif;
                j2Actif.Foreground = couleurInactif;
            }
            else
            {
                j1Actif.Foreground = couleurInactif;
                j2Actif.Foreground = couleurActif;
            }



        }


        public void refreshStatUniteCourante()
        {
            if (SmallWorld.Instance.uniteCourante != -1)
            {
                Unite u = SmallWorld.Instance.getUniteCourante();
                imgUnitCour.Source = App.getImageFromPeuple(SmallWorld.Instance.joueurs[SmallWorld.Instance.getJoueurCourant().idJoueur].Peuple);
                
                
                // ID_UnitCourr.Text = "Unité n°" + SmallWorld.Instance.uniteCourante;
                atkUnitCourr.Text = u.attaque.ToString();
                defUnitCourr.Text = u.defense.ToString();
                vieUnitCourr.Text = u.vie.ToString();
                GridUniteActive.Visibility = System.Windows.Visibility.Visible;

            } else {
                ID_UnitCourr.Text = "Aucune unité selectionée";
                GridUniteActive.Visibility = System.Windows.Visibility.Hidden;
            }
            
        }


        public void setInfoUniteSurvole(Unite u, int id)
        {
            //ID_UnitSurv.Text = "Unité n°" + id;

            // TODO : Ca marche pas
            Console.WriteLine(u.getPeuple());
            imgUnitSurv.Source = App.getImageFromPeuple(u.getPeuple());
  
            atkUnitSurv.Text = u.attaque.ToString();
            defUnitSurv.Text = u.defense.ToString();
            vieUnitSurv.Text = u.vie.ToString();

            GridUniteSurv.Visibility = System.Windows.Visibility.Visible;
            //img_UnitSurv.Visibility = System.Windows.Visibility.Visible;
            //all_InfoUnitSurv.Visibility = System.Windows.Visibility.Visible;
        }


        public void setInfoUniteSurvole(int s)
        {
            GridUniteSurv.Visibility = System.Windows.Visibility.Hidden;
            //all_InfoUnitSurv.Visibility = System.Windows.Visibility.Visible;
            //img_UnitSurv.Visibility = System.Windows.Visibility.Visible;
        }

        public void hideInfoUniteSurvole()
        {
            GridUniteSurv.Visibility = System.Windows.Visibility.Hidden;
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