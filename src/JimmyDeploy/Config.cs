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
        
        public string compName { get; set; }
        public string compDesc { get; set; }

        public List<Data.Task> tasks { get; set; }
        public bool autoLogin { get; set; }

        private string configFile;

        public string configLocation
        {
            get
            {
                return configFile;
            }
            set
            {
                configFile = value;
                data = null;
                data = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(value));
            }
        }

        private Config()
        {
            configLocation = @"JimmyDeploy.json";

            data.applications = data.applications.OrderBy(o => o.order).ToList();
        }

        public static Config get()
        {
            if (data == null)
            {
                config = new Config();
            }
            return config;
        }

        public DomainInfo getDomainInfo()
        {
            return data.domainInfo;
        }

        public List<Application> getApplications()
        {
            return data.applications;
        }

        public List<Data.Task> GetTasks()
        {
            return tasks;
        }

        public void setupTaskList()
        {
            tasks = new List<Data.Task>();
            int order = 0;
            foreach (string s in data.order)
            {
                switch (s)
                {
                    case "applicationsPreDomain":
                        foreach (Application a in data.applications)
                        {
                            if (a.beforeDomain)
                            {
                                tasks.Add(new Data.Task { order = order, name = "Install App", details = a.name, progress = "", taskObj = a });
                            }
                        }
                        order++;
                        break;
                    case "reboot":
                        tasks.Add(new Data.Task { order = order, name = "Reboot", details = "", progress = "", });
                        order++;
                        break;
                    case "applications":
                        foreach (Application a in data.applications)
                        {
                            if (!a.beforeDomain)
                            {
                                tasks.Add(new Data.Task { order = order, name = "Install App", details = a.name, progress = "", taskObj = a });
                                order++;
                            }
                        }
                        break;
                    case "domainJoin":
                        string domainDetails = string.Format("Name: {0} Username: {1}", data.domainInfo.Name, data.domainInfo.Username);
                        tasks.Add(new Data.Task { order = order, name = "Join Domain", details = domainDetails, progress = "" });
                        order++;
                        break;
                    case "computerName":
                        string computerDetails = string.Format("Name: {0} Description: {1}", compName, compDesc);
                        tasks.Add(new Data.Task { order = order, name = "Rename Computer", details = computerDetails, progress = "" });
                        order++;
                        break;
                }
            }
        }
    }
}
