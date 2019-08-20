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
            domainInfo = Config.get().getDomainInfo();
            if (domainInfo.Name != null)
            {
                DomainBox.Text = domainInfo.Name;
                DomainBox.IsEnabled = false;
            }
            if (domainInfo.Username != null)
            {
                UsernameBox.Text = domainInfo.Username;
                UsernameBox.IsEnabled = false;
            if (domainInfo.Password != null)
            {
                PasswordBox.Password = domainInfo.Password;
                PasswordBox.IsEnabled = false;
            }
            }
        }

        private void UsernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            domainInfo.Username = UsernameBox.Text;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            domainInfo.Password = PasswordBox.Password;
        }
    }
}
