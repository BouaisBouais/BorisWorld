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

        public CanvasCarte() : base()
        {
            int taille = Carte.taille;
            this.Height = taille * imgSize;
            this.Width = taille * imgSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            try
            {
                drawBack(dc);
                drawUnits(dc);
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

        private void drawUnits(DrawingContext dc)
        {

            Coordonnee currentUnit = SmallWorld.Instance.getUniteCourante().coordonnees;
            SolidColorBrush scb = Brushes.Red;
            int x = (currentUnit.getX() - 1) * imgSize + 25;
            int y = (currentUnit.getY() - 1) * imgSize + 25;
            dc.DrawEllipse(scb, null, new Point(x, y), 14, 14);

            foreach (Joueur joueur in SmallWorld.Instance.joueurs)
            {
                foreach (Unite unite in joueur.getUnites())
                {
                    Coordonnee coords = unite.coordonnees;
                    SolidColorBrush color = App.getColorFromPeuple(joueur.Peuple);
                    int ellipseX = (coords.getX() - 1) * imgSize + 25;
                    int ellipseY = (coords.getY() - 1) * imgSize + 25;
                    dc.DrawEllipse(color, null, new Point(ellipseX, ellipseY), 12, 12);

                    int nb = Carte.getNombreUnites(coords);
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
    }
}
