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
        private List<Data.Task> tasks;

        public bool restartRequired { get; set; }

        private Config()
        {
            data = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(@"C:\Users\james\Desktop\example.json"));
            restartRequired = false;

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

        public List<Data.Task> getTaskList()
        {
            if (tasks == null)
            {
                setupTaskList();
            }
            return tasks;
        }

        private void setupTaskList()
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
                                tasks.Add(new Data.Task { order = order, name = "Install App", details = a.name, progress = "" });
                            }
                        }
                        order++;
                        break;
                    case "reboot":
                        tasks.Add(new Data.Task { order = order, name = "Reboot", details = "", progress = "" });
                        order++;
                        break;
                    case "applications":
                        foreach (Application a in data.applications)
                        {
                            if (!a.beforeDomain)
                            {
                                tasks.Add(new Data.Task { order = order, name = "Install App", details = a.name, progress = "" });
                            }
                        }
                        order++;
                        break;
                    case "domainJoin":
                        string domainDetails = string.Format("Name: {0} Username: {1}", data.domainInfo.name, data.domainInfo.username);
                        tasks.Add(new Data.Task { order = order, name = "Join Domain", details = domainDetails, progress = "" });
                        order++;
                        break;
                }
            }
        }
    }
}
