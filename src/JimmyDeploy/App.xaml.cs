using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JimmyDeploy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            int step = 0;
            if (e.Args.Length == 1)
                step = int.Parse(e.Args[0]);
            MainWindow wnd = new MainWindow(step);
            wnd.Show();
        }
    }
}
