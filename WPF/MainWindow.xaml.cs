using Small_World;
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

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SmallWorld smallworld;

        public MainWindow()
        {
            InitializeComponent();
            smallworld = new SmallWorld();
            dessinerCarte();
        }

        unsafe public void dessinerCarte()//int** carte)
        {
            double width = canvasCarte.RenderSize.Width;
            double height = canvasCarte.ActualHeight;
            Console.WriteLine("\nTaille Grid : " + width + ", " + height);
        }
    }
}
