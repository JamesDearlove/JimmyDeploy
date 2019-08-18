using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAutoSetup.Data
{
    public class Branding
    {
        public string name { get; set; }
        public string logo { get; set; }
    }

    public class DomainInfo
    {
        public string name { get; set; }
        public object username { get; set; }
        public object password { get; set; }
        public bool reboot { get; set; }
    }

    public class Application
    {
        public string name { get; set; }
        public string command { get; set; }
        public bool reboot { get; set; }
        public bool beforeDomain { get; set; }
        public int order { get; set; }
        public string download { get; set; }
    }

    public class RootObject
    {
        public int version { get; set; }
        public Branding branding { get; set; }
        public DomainInfo domainInfo { get; set; }
        public List<Application> applications { get; set; }
        public List<string> order { get; set; }
    }
}
