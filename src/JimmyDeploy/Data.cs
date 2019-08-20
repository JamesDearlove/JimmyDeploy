using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyDeploy.Data
{
    public class Branding
    {
        public string name { get; set; }
        public string logo { get; set; }
    }

    public class DomainInfo
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Reboot { get; set; }
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

    public class Task : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string privDetail;
        private string privProg;
        public int order { get; set; }
        public string name { get; set; }
        public string details
        {
            get
            {
                return privDetail;
            }
            set
            {
                privDetail = value;
                OnPropertyChanged("details");
            }
        }
        public object taskObj { get; set; }
        public string progress
        {
            get
            {
                return privProg;
            }
            set
            {
                privProg = value;
                OnPropertyChanged("progress");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
