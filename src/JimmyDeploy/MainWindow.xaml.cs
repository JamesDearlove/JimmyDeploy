using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Media.Animation;

namespace JimmyDeploy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Page[] navigationOrder = new Page[] { new WelcomePage(), new ComputerSettingsPage(), new DomainPage(), new AppsPage() };
        public int navigationLoc = 0;

        public MainWindow()
        {
            InitializeComponent();
            _mainFrame.NavigationService.Navigate(navigationOrder[0]);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            navigationLoc += 1;
            _mainFrame.NavigationService.Navigate(navigationOrder[navigationLoc]);
            if (navigationLoc + 1 >= navigationOrder.Length)
            {
                NextButton.IsEnabled = false;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.GoBack();
            navigationLoc--;
            if (navigationLoc < navigationOrder.Length)
            {
                NextButton.IsEnabled = true;
            }
        }

        private void _mainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            BackButton.IsEnabled = _mainFrame.NavigationService.CanGoBack;
            if (Config.get().restartRequired)
            {
                RestartText.Visibility = Visibility.Visible;
            } else
            {
                RestartText.Visibility = Visibility.Hidden;
            }
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult result = await this.ShowMessageAsync("Exiting", "Are you sure you want to cancel?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        public void enableNextBtn(bool value)
        {
            NextButton.IsEnabled = value;
        }

        private async void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            MessageDialogResult result = await this.ShowMessageAsync("Exiting", "Are you sure you want to cancel?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private async void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Version version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            string currentV = string.Format("Version: {0}.{1}", version.Major, version.Minor);
            await this.ShowMessageAsync("About", "Jimmy Deployer - Written by James Dearlove \n" + currentV, MessageDialogStyle.Affirmative);
        }
    }
}
