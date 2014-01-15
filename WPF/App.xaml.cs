using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Small_World;
using WPF.Menu;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        

        void App_startup(Object sender, StartupEventArgs e)
        {

            MainWindow window = new MainWindow();
            window.Show();


        }

        public static BitmapImage getImageFromPeuple(TypeUnite peuple)
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();

            switch (peuple)
            {
                case TypeUnite.Gaulois:
                    logo.UriSource = new Uri("pack://application:,,,/WPF;component/Ressources/gaulois.png");
                    break;
                case TypeUnite.Nain:
                    logo.UriSource = new Uri("pack://application:,,,/WPF;component/Ressources/nain.png");
                    break;
                case TypeUnite.Viking:
                    logo.UriSource = new Uri("pack://application:,,,/WPF;component/Ressources/viking.png");
                    break;
            }

            logo.EndInit();
            return logo;
            
        }

        public static SolidColorBrush getColorFromPeuple(TypeUnite peuple)
        {
            
            SolidColorBrush color = Brushes.Black;
            switch (peuple)
            {
                case TypeUnite.Gaulois:
                    color = Brushes.Yellow;
                    break;
                case TypeUnite.Nain:
                    color = Brushes.SlateGray;
                    break;
                case TypeUnite.Viking:
                    color = Brushes.Purple;
                    break;
            }
            return color;
        }

        public static String getNameFromPeuple(TypeUnite peuple)
        {
            String name = "";
            switch (peuple)
            {
                case TypeUnite.Gaulois:
                    name = "Gaulois";
                    break;
                case TypeUnite.Nain:
                    name = "Nain";
                    break;
                case TypeUnite.Viking:
                    name = "Viking";
                    break;
            }
            return name;
        }
    }
}