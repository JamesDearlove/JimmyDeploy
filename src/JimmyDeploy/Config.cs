using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JimmyDeploy.Data;
using System.Collections.ObjectModel;

namespace JimmyDeploy
{
    public class Config
    {
        private static Config config;
        private static RootObject data;
        
        public string CompName { get; set; }
        public string CompDesc { get; set; }

        public ObservableCollection<Data.Step> Steps { get; set; }
        public bool AutoLogin { get; set; }

        private string ConfigFile { get; set; }

        public string ConfigLocation
        {
            get
            {
                return this.ConfigFile;
            }
            set
            {
                this.ConfigFile = value;
                data = null;
                data = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(value));

                Steps = data.steps;
            }
        }

        private Config()
        {
            ConfigLocation = @"JimmyDeploy.json";

            //data.installers = data.installers.OrderBy(o => o.id).ToList();
        }

        public static Config get()
        {
            if (data == null)
            {
                config = new Config();
            }
            return config;
        }
    }
}
