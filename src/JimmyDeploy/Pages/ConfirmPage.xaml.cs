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
    public partial class ConfirmPage : Page
    {
        public ConfirmPage()
        {
            InitializeComponent();

            //Config.get().setupTaskList();
            //DataContext = Config.get().tasks;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: Fix this
        }

    }
}
