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

namespace WPF.FinJeu
{
    /// <summary>
    /// Interaction logic for FinJeu.xaml
    /// </summary>
    public partial class FinJeu : Page
    {
        public FinJeu()
        {
            InitializeComponent();

            Joueur gagnant = SmallWorld.Instance.getVainqueur();

            vainqueur.Foreground = App.getColorFromPeuple(gagnant.Peuple);
            vainqueur.Text = App.getNameFromPeuple(gagnant.Peuple);

            points.Foreground = Brushes.White;
            points.Text = gagnant.getPoints().ToString();
        }
    }
}
