using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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

namespace JimmyDeploy
{
    /// <summary>
    /// Interaction logic for ComputerSettingsPage.xaml
    /// </summary>
    public partial class ComputerSettingsPage : Page
    {
        public string name;
        public string description;

        public ComputerSettingsPage()
        {
            InitializeComponent();
            getData();
            RetrieveComputerProps();
        }

        public void getData()
        {
            string compName = Environment.MachineName;
            CompNameBox.Text = compName;
        }

        public static void RetrieveComputerProps()
        {
            //initialize the select query with command text
            SelectQuery query = new SelectQuery(@"Select * from Win32_ComputerSystem");

            //initialize the searcher with the query it is supposed to execute
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                //execute the query
                foreach (ManagementObject process in searcher.Get())
                {
                    //print system info
                    process.Get();
                    Console.WriteLine("{0}{1}", "Caption :", process["Caption"]);
                    Console.WriteLine("{0}{1}", "Caption :", process["Description"]);
                }
            }
        }

        private void CompNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.get().compName = CompNameBox.Text;
        }

        private void CompDescBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.get().compDesc = CompDescBox.Text;
        }
    }
}
