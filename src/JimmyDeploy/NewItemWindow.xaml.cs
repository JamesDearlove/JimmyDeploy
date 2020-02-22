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
using System.Windows.Shapes;
using MahApps.Metro.SimpleChildWindow;
using MahApps.Metro.SimpleChildWindow.Utils;

namespace JimmyDeploy
{
    /// <summary>
    /// Interaction logic for NewItemWindow.xaml
    /// </summary>
    public partial class NewItemWindow : ChildWindow
    {
        public NewItemWindow()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var newStep = new Data.Step
            {
                name = TextBoxName.Text,
                type = "RunInstaller",
                installertype = ComboBoxSetupType.Text,
                setupfile = TextBoxFile.Text,
                arguments = TextBoxArgs.Text
            };

            this.Close(newStep);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(null);
        }
    }
}
