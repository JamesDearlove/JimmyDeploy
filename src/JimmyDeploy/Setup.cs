using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JimmyDeploy.Data;

namespace JimmyDeploy
{
    public class Setup
    {

        public static void start()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                Console.WriteLine("Hello, world");
            }).Start();
        }

        public static void enableAutoLogin()
        {

        }

        public static void disableAutoLogin()
        {

        }

        public static void joinDomain()
        {
            DomainInfo info = Config.get().getDomainInfo();
        }
    }
}
