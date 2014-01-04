using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Small_World;
using WPF.Menu;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SmallWorld gameEngine = new SmallWorld();

        void App_startup(Object sender, StartupEventArgs e)
        {

            MainWindow window = new MainWindow();
            window.Show();


        }
    }
}
