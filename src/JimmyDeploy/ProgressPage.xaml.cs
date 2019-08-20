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

using JimmyDeploy.Data;

namespace JimmyDeploy
{
    /// <summary>
    /// Interaction logic for AppsPage.xaml
    /// </summary>
    public partial class ProgressPage : Page
    {
        public ProgressPage(int step)
        {
            InitializeComponent();

            if (step != 0)
            {
                for (int i = 0; i < step; i++)
                {
                    Config.get().tasks[i].progress = "Done";
                }
            }
            DataContext = Config.get().tasks;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.enableBackBtn(false);
            mainWindow.enableNextBtn(false);
            mainWindow.enableIndicator(true);

            Setup.start();
        }

        private void AppsList_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            MessageBox.Show("hi");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Config.get().tasks[0].progress = "Yeet";
        }
    }
}
