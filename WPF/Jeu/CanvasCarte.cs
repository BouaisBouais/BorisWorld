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


        private Dictionary<Point,Coordonnee> pointsCourrants;
        private const int radiusPoints = 12;
        StackPanel s = new StackPanel();
        private List<Unite> selectionCourrante = new List<Unite>();


        private Coordonnee caseSurvolee = null;
        private Coordonnee lastCaseSurvolee = null;

        public CanvasCarte() : base()
        {
            int taille = Carte.taille;
            this.Height = taille * imgSize;
            this.Width = taille * imgSize;
            this.pointsCourrants = new Dictionary<Point,Coordonnee>();

            this.Children.Add(s);
            s.Visibility = System.Windows.Visibility.Hidden;
        }

        protected override void OnRender(DrawingContext dc)
        {
            try
            {
                drawBack(dc);
                drawSuggestions(dc);
                drawUnits(dc);
                drawDeplacement(dc);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Processor Usage" + ex.Message);
            }
        }

        private void drawBack(DrawingContext dc)
        {
            int taille = Carte.taille;
            for (int x = 1; x <= taille; x++)
            {
                for (int y = 1; y <= taille; y++)
                {
                    Coordonnee coords = new Coordonnee(x, y);
                    switch (Carte.getCase(coords).getTypeCase())
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
            foreach (Coordonnee c in Carte.getSuggestions())
            {
                drawCase(c, Brushes.Green.Clone(), 0.35, dc);
            }
        }

       

        private void drawUnits(DrawingContext dc)
        {
            pointsCourrants.Clear();
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


                    int nb = Carte.getNombreUnites(coords);

                    if (!pointsCourrants.ContainsKey(p) && joueur == SmallWorld.Instance.getJoueurCourant())
                        pointsCourrants.Add(p, coords);
                    

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

            caseSurvolee = null;
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
                verifierActionDeplacement(click);
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
                if (!(tempCaseSurvol.Equals(uCourrante.coordonnees)) &&
                uCourrante.deplacementPossible(tempCaseSurvol))
                {
                    caseSurvolee = tempCaseSurvol;
                }
                else
                {
                    caseSurvolee = null;
                }
                lastCaseSurvolee = tempCaseSurvol;

                // Survol d'une unitée


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
            bool panelAffiche = false;

            foreach (Point p in pointsCourrants.Keys)
            {
                Vector v = Point.Subtract(p, click);
                int x = Math.Abs((int)v.X);
                int y = Math.Abs((int)v.Y);

                // SI l'utilisateur à cliqué sur une unitée
                if (x < radiusPoints && y < radiusPoints)
                {
                    Coordonnee c = pointsCourrants[p];
                    Dictionary<Unite, int> dUnite = Carte.getUnites(c);
                    
                    // Appartition d'un menu si plusieurs unitées
                    if (dUnite.Count > 1)
                    {
                        s.Children.Clear();
                        foreach (Unite u in dUnite.Keys)
                        {
                            Button b = new Button();
                            b.Content = "Unité n°" + dUnite[u];
                            b.Click += changerUniteCourante_Click;

                            if (u.mouvement == 0)
                                b.IsEnabled = false;

                            b.Name = "x" + dUnite[u];
                            s.Children.Add(b);
                        }

                        s.Arrange(new Rect(click, new Size(200, 150)));
                        s.Visibility = System.Windows.Visibility.Visible;
                        panelAffiche = true;
                    }
                    else
                    {
                        if (dUnite.Keys.ElementAt(0).mouvement > 0)
                            changeUniteCourante(dUnite.Values.ElementAt(0));
                    }
                }
            }

            if (!panelAffiche)
                s.Visibility = System.Windows.Visibility.Hidden;
        }



        public void changerUniteCourante_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            try
            {
                string nom = b.Name.Substring(1);
                int id = int.Parse(nom);
                changeUniteCourante(id);
            }
            catch
            {
                    Console.WriteLine("Erreur de parsing lors de la selection d'une unitée courante");
            }
            


        }

        public void changeUniteCourante(int id)
        {

            SmallWorld.Instance.uniteCourante = id;
            s.Visibility = System.Windows.Visibility.Hidden;
            Console.WriteLine("Changement d'unité courrante : {0}", SmallWorld.Instance.uniteCourante);

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
