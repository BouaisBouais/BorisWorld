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

namespace WPF.RapportCombat
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class RapportCombatDialog : Window
    {
        public RapportCombatDialog(Log l)
        {
            InitializeComponent();

            foreach (string s in l.logCombat)
            {
                Grid g = new Grid();

                PanelResult.Children.Add(g);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
