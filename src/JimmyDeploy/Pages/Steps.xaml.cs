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
using MahApps.Metro.SimpleChildWindow;
using MahApps.Metro.SimpleChildWindow.Utils;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace JimmyDeploy.Pages
{
    /// <summary>
    /// Interaction logic for Steps.xaml
    /// </summary>
    public partial class Steps : Page
    {
        public Steps()
        {
            InitializeComponent();

            DataContext = Config.get().Steps;
            Console.WriteLine(Config.get().Steps);
        }

        private async void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            var newItem = await mainWindow.OpenNewItemWindowAsync();

            Config.get().Steps.Add(newItem);
        }

        private void ButtonRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Data.Step) ListViewSteps.SelectedItem;
            var selectedIndex = ListViewSteps.SelectedIndex;

            Config.get().Steps.Remove(selectedItem);
            ListViewSteps.SelectedIndex = selectedIndex;
        }

        private void ButtonExportConfig_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON File (*.json)|*.json|All Files (*.*)|*.*";
            saveFileDialog.FileName = "JimmyDeploy.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                var steps = Config.get().Steps;
                var rootObject = new Data.RootObject
                {
                    steps = steps
                };
                var stepSerialized = JsonConvert.SerializeObject(rootObject, Formatting.Indented, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
                File.WriteAllText(saveFileDialog.FileName, stepSerialized);
            }
        }

        private void ButtonMoveUp_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(-1);
        }

        private void ButtonMoveDown_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(+1);
        }

        private void MoveItem(int relPos)
        {
            var selectedItem = (Data.Step)ListViewSteps.SelectedItem;
            var cntLoc = Config.get().Steps.IndexOf(selectedItem);
            var newLoc = cntLoc + relPos;

            if (newLoc >= 0 && newLoc < Config.get().Steps.Count)
            {
                Config.get().Steps.Move(cntLoc, newLoc);
            }
        }
    }
}
