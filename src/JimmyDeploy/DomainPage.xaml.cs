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
    /// Interaction logic for DomainPage.xaml
    /// </summary>
    public partial class DomainPage : Page
    {

        DomainInfo domainInfo;

        public DomainPage()
        {
            InitializeComponent();
            getData();
        }

        private void getData()
        {
            domainInfo = Config.get().domainInfo();
            if (domainInfo.name != null)
            {
                DomainBox.Text = domainInfo.name;
                DomainBox.IsEnabled = false;
            }
            if (domainInfo.username != null)
            {
                UsernameBox.Text = domainInfo.username;
                UsernameBox.IsEnabled = false;
            if (domainInfo.password != null)
            {
                PasswordBox.Password = domainInfo.password;
                PasswordBox.IsEnabled = false;
            }
            }
        }
    }
}
