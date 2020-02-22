using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyDeploy.Data
{
    public class Step
    {
        public string type { get; set; }
        public bool completed { get; set; }

        // Name Change Parameters
        public string @default { get; set; }

        // Domain Parameters
        public string domain  { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        // Copy Parameters
        public string source { get; set; }
        public string destination { get; set; }

        // Installer Parameters
        public string name { get; set; }
        public string installertype { get; set; }
        public string setupfile { get; set; }
        public string arguments { get; set; }


        [JsonIgnore]
        public string niceType
        {
            get
            {
                switch (type)
                {
                    case "DomainJoin":
                        return "Join Domain";
                    case "NameChange":
                        return "Change Computer Name";
                    case "Reboot":
                        return "Reboot Computer";
                    case "RunInstaller":
                        return "Install App";
                    case "CopyFolder":
                        return "Copy Folder";
                    default:
                        return "Unrecognised Command";
                } 
            }
        }

        [JsonIgnore]
        public string details
        {
            get
            {
                switch (type)
                {
                    case "DomainJoin":
                        return "Joining Domain: " + domain;
                    case "NameChange":
                        return "";
                    case "Reboot":
                        return "";
                    case "RunInstaller":
                        return name;
                    case "CopyFolder":
                        return source + " to " + destination;
                    default:
                        return type + " is not recognised as a command";
                }
            }
        }
    }

    public class RootObject
    {
        public ObservableCollection<Step> steps { get; set; }
    }
}
