using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;
using Small_World;

namespace WPF.Jeu
{
    public partial class CanvasCarte : Canvas
    {
        private BitmapImage desert = new BitmapImage(new Uri(@"Ressources/terrains/desert.png", UriKind.RelativeOrAbsolute));
        private BitmapImage montagne = new BitmapImage(new Uri(@"Ressources/terrains/montagne.png", UriKind.RelativeOrAbsolute));
        private BitmapImage eau = new BitmapImage(new Uri(@"Ressources/terrains/eau.png", UriKind.RelativeOrAbsolute));
        private BitmapImage plaine = new BitmapImage(new Uri(@"Ressources/terrains/plaine.png", UriKind.RelativeOrAbsolute));
        private BitmapImage foret = new BitmapImage(new Uri(@"Ressources/terrains/forest.gif", UriKind.RelativeOrAbsolute));
        private const int imgSize = 50;


        private const int radiusPoints = 12;
        StackPanel s = new StackPanel();

        private Coordonnee caseSurvolee = null;
        private Coordonnee lastCaseSurvolee = null;
        private Jeu fenetreParent = null;


        public CanvasCarte() : base()
        {
            int taille = SmallWorld.Instance.carte.taille;
            this.Height = taille * imgSize;
            this.Width = taille * imgSize;
            this.Children.Add(s);
            s.Visibility = System.Windows.Visibility.Hidden;
        }

        public void setFenetreParent(Jeu j)
        {
            fenetreParent = j;
        }

        protected override void OnRender(DrawingContext dc)
        {
            try
            {
                drawBack(dc);
                drawSuggestions(dc);
                drawUnits(dc);
                drawDeplacement(dc);
                if (fenetreParent != null)
                {
                    fenetreParent.refreshStatUniteCourante();
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Processor Usage" + ex.Message);
            }
        }


   
        private void drawBack(DrawingContext dc)
        {
            int taille = SmallWorld.Instance.carte.taille;
            for (int x = 1; x <= taille; x++)
            {
                for (int y = 1; y <= taille; y++)
                {
                    Coordonnee coords = new Coordonnee(x, y);
                    switch (SmallWorld.Instance.carte.getCase(coords).getTypeCase())
                    {
                        case TypeCases.DESERT:
                            dc.DrawImage(desert, new Rect((x - 1) * imgSize, (y - 1) * imgSize, imgSize, imgSize));
                            break;
                        case TypeCases.PLAINE:
                            dc.DrawImage(plaine, new Rect((x - 1) * imgSize, (y - 1) * imgSize, imgSize, imgSize));
                            break;
                        case TypeCases.MONTAGNE:
                            dc.DrawImage(montagne, new Rect((x - 1) * imgSize, (y - 1) * imgSize, imgSize, imgSize));
                            break;
                        case TypeCases.EAU:
                            dc.DrawImage(eau, new Rect((x - 1) * imgSize, (y - 1) * imgSize, imgSize, imgSize));
                            break;
                        case TypeCases.FORET:
                            dc.DrawImage(foret, new Rect((x - 1) * imgSize, (y - 1) * imgSize, imgSize, imgSize));
                            break;
                    }
                }
            }
        }

        private void drawSuggestions(DrawingContext dc)
        {
            foreach (Coordonnee c in SmallWorld.Instance.carte.getSuggestions())
            {
                drawCase(c, Brushes.Green.Clone(), 0.35, dc);
            }
        }

       

        private void drawUnits(DrawingContext dc)
        {
            Coordonnee currentUnit = SmallWorld.Instance.getUniteCourante().coordonnees;
            SolidColorBrush scb = Brushes.Red;
            int x = (currentUnit.getX() - 1) * imgSize + 25;
            int y = (currentUnit.getY() - 1) * imgSize + 25;
            dc.DrawEllipse(scb, null, new Point(x, y), radiusPoints + 2, radiusPoints +2);


            
            foreach (Joueur joueur in SmallWorld.Instance.joueurs)
            {
                foreach (Unite unite in joueur.getUnites())
                {
                    Coordonnee coords = unite.coordonnees;
                    
                    SolidColorBrush color = App.getColorFromPeuple(joueur.Peuple);
                    int ellipseX = (coords.getX() - 1) * imgSize + 25;
                    int ellipseY = (coords.getY() - 1) * imgSize + 25;
                    
                    Point p = new Point(ellipseX, ellipseY);
                   
                    dc.DrawEllipse(color, null, p, radiusPoints, radiusPoints);


                    int nb = SmallWorld.Instance.carte.getNombreUnites(coords);

                    if (nb > 1)
                    {
                        FormattedText text = new FormattedText(nb.ToString(),
                            CultureInfo.GetCultureInfo("fr-fr"),
                            FlowDirection.LeftToRight,
                            new Typeface("Verdana"),
                            15,
                            Brushes.White);
                        Geometry border = text.BuildGeometry(new Point(ellipseX - 5, ellipseY - 10));
                        dc.DrawGeometry(null, new Pen(Brushes.Black, 3), border);
                        dc.DrawText(text, new Point(ellipseX - 5, ellipseY - 10));
                    }
                }
            }
        }



        private void drawDeplacement(DrawingContext dc)
        {
            if (caseSurvolee != null) {
                drawCase(caseSurvolee, Brushes.Blue.Clone(), 0.35, dc);
                caseSurvolee = null;
            }
            

        }




        private void drawCase(Coordonnee c, Brush couleur, double opacity, DrawingContext dc)
        {
            int x = c.getX();
            int y = c.getY();
            Brush sugg = couleur;
            sugg.Opacity = opacity;
            dc.DrawRectangle(sugg, null, new Rect((x - 1) * imgSize, (y - 1) * imgSize, imgSize, imgSize));
        }


        private bool checkPreventRefresh()
        {
            if (s.Visibility == System.Windows.Visibility.Visible)
                return true;
            return false;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {

            if (checkPreventRefresh())
                return;

            lastCaseSurvolee = null;
            this.InvalidateVisual();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Point click = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed) {
                verifierActionSelectionUnite(click);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
               
                if (s.Visibility == System.Windows.Visibility.Hidden)
                {
                    
                    verifierActionDeplacement(click);

                    lastCaseSurvolee = null; // Permet de refresh l'interface
                    MouseEventArgs mouse1 = new MouseEventArgs(Mouse.PrimaryDevice, 0) { RoutedEvent = Mouse.MouseMoveEvent };
                    this.RaiseEvent(mouse1);
                }
                else  // Si jamais le menu est ouvert on le ferme juste et on déplace pas
                {
                    s.Visibility = System.Windows.Visibility.Hidden;
                    this.InvalidateVisual();
                }
                
          
            }
            
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

            if (checkPreventRefresh())
                return;
            

            Point click = e.GetPosition(this);
            Coordonnee tempCaseSurvol = pointToCoordonne(click);
            Unite uCourrante = SmallWorld.Instance.getUniteCourante();


            if (lastCaseSurvolee == null || (!(tempCaseSurvol.Equals(lastCaseSurvolee))))
            {
                // Affichage d'une case déplacable
                if (uCourrante.deplacementPossible(tempCaseSurvol))
                {

                    caseSurvolee = tempCaseSurvol;
                }
                else
                {
                    caseSurvolee = null;
                }
                

                // Survol d'une unité
                // TODO : rendre la classe carte plus propre
                Dictionary<Unite, int> d = SmallWorld.Instance.carte.getUnites(tempCaseSurvol);
                Console.WriteLine("COUNT : {0}", d.Count);
                if (d.Count == 1)
                {
                    if (fenetreParent != null)
                        fenetreParent.setInfoUniteSurvole(d.Keys.First(), d.Values.First());
                }
                else if (d.Count > 1)
                {
                    if (fenetreParent != null)
                        fenetreParent.setInfoUniteSurvole(d.Count);
                }
                else
                {
                    if (fenetreParent != null)
                     fenetreParent.hideInfoUniteSurvole();
                }


                lastCaseSurvolee = tempCaseSurvol;
                this.InvalidateVisual();
            }

         }
            

        public void verifierActionDeplacement(Point click)
        {

            Coordonnee arrivee = pointToCoordonne(click);
            bool finJeu = SmallWorld.Instance.deplacement(arrivee);
            this.InvalidateVisual();
            if (finJeu) {
                MainWindow m = (MainWindow)Application.Current.MainWindow;
                m.afficherFinJeu();
            }



        }



        // Verifie si une unitée est sur la case ou non.
        public void verifierActionSelectionUnite(Point click)
        {
            Coordonnee caseClique = pointToCoordonne(click);

            Dictionary<Unite, int> unites = SmallWorld.Instance.carte.getUnites(caseClique);

            if (unites.Count == 1)
            { 
                if (unites.Keys.First().mouvement > 0)
                    changeUniteCourante(unites.Values.First());
                s.Visibility = System.Windows.Visibility.Hidden;

            }
            else if (unites.Count > 1)
            {
                s.Children.Clear();

                foreach (Unite u in unites.Keys)
                {
                    Button b = new Button();
                    b.Content = "Unité n°" + unites[u];
                    b.Click += changerUniteCourante_Click;
                    b.MouseMove += setDetailUnit;
                    b.MouseLeave += hideDetailUnit;

                    if (u.mouvement == 0)
                        b.IsEnabled = false;

                    b.Name = "x" + unites[u];
                    s.Children.Add(b);
                }

                s.Arrange(new Rect(click, new Size(200, 150)));
                s.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                s.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void setDetailUnit(object sender, RoutedEventArgs e)
        {
            int id = recupererIDUnite((Button)sender);
            if (id != -1)
            {
                if (fenetreParent != null)
                    fenetreParent.setInfoUniteSurvole(SmallWorld.Instance.getJoueurCourant().getUnites().ElementAt(id), id);
            }
        }

        public void hideDetailUnit(object sender, RoutedEventArgs e)
        {
            if (fenetreParent != null)
                fenetreParent.hideInfoUniteSurvole();
        }

        public void changerUniteCourante_Click(object sender, RoutedEventArgs e)
        {
            changeUniteCourante(recupererIDUnite((Button)sender));
        }


        public int  recupererIDUnite(Button b)
        {
            try
            {
                string nom = b.Name.Substring(1);
                return int.Parse(nom);
            }
            catch
            {
                Console.WriteLine("Erreur de parsing lors de la selection d'une unitée courante");
                return -1;
            }
            
        }

        public void changeUniteCourante(int id)
        {

            SmallWorld.Instance.uniteCourante = id;
            s.Visibility = System.Windows.Visibility.Hidden;
            this.InvalidateVisual();
        }


        public Coordonnee pointToCoordonne(Point p)
        {
            Point temp = p;
            int x= 1 , y = 1;

            while (p.X - imgSize > 0)
            {
                p.X -= imgSize;
                x++;
            }

            while (p.Y - imgSize > 0)
            {
                p.Y -= imgSize;
                y++;
            }



            return new Coordonnee(x,y);
        }
    }
}
