using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JimmyDeploy.Data;

namespace JimmyDeploy
{
    public class Config
    {
        private static Config config;
        private static RootObject data;

        public bool restartRequired { get; set; }

        private Config()
        {
            data = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(@"C:\Users\james\Desktop\example.json"));
            restartRequired = false;   
        }

        public static Config get()
        {
            if (data == null)
            {
                config = new Config();
            }
            return config;
        }

        public DomainInfo domainInfo()
        {
            return data.domainInfo;
        }
    }
}
