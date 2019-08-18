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

using WindowsAutoSetup.Data;
using MahApps.Metro.Controls;


namespace WindowsAutoSetup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private RootObject data;

        private DomainInfo domainInfo;

        public String domainString { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // read file into a string and deserialize JSON to a type
            data = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(@"C:\Users\james\Desktop\example.json"));
            domainInfo = data.domainInfo;
            domainString = domainInfo.name;
            MessageBox.Show(domainInfo.ToString());
        }
    }
}
