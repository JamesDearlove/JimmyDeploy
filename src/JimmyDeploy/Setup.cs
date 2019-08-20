using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JimmyDeploy.Data;

namespace JimmyDeploy
{
    public class Setup
    {
        static int step = 0;

        public static void start()
        {
            new Thread(() =>
            {
                foreach (Data.Task t in Config.get().tasks)
                {
                    t.progress = "Running";
                    bool result = false;
                    switch (t.name)
                    {
                        case "Reboot":
                            result = startReboot();
                            break;
                        case "Install App":
                            result = installApp((Application)t.taskObj);
                            break;
                        case "Join Domain":
                            result = joinDomain();
                            break;
                        case "Rename Computer":
                            //result = changeComputerInfo();
                            break;
                    }
                    t.progress = result ? "Complete" : "Failed";
                }
                

                Console.WriteLine("Hello, world");
            }).Start();
        }

        public static bool enableAutoLogin()
        {
            return false;
        }

        public static bool disableAutoLogin()
        {
            return false;
        }

        public static bool changeComputerInfo()
        {
            bool result = true;
            if (!changeComputerName())
            {
                result = false;
            }
            if (!changeComputerDescription())
            {
                result = false;
            }
            return result;
        }

        public static bool changeComputerName()
        {
            // Create a new process
            ProcessStartInfo process = new ProcessStartInfo();

            // set name of process to "WMIC.exe"
            process.FileName = "WMIC.exe";

            // pass rename PC command as argument
            process.Arguments = "computersystem where caption='" + System.Environment.MachineName + "' rename " + Config.get().compName;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(process))
            {
                proc.WaitForExit();

                // print the status of command
                Console.WriteLine("Exit code = " + proc.ExitCode);
                return proc.ExitCode == 0 ? true : false;
            }
        }

        public static bool changeComputerDescription()
        {
            ProcessStartInfo process = new ProcessStartInfo();
            process.FileName = "net.exe";

            process.Arguments = "config server /srvcomment:'" + Config.get().compDesc + "'";

            using (Process proc = Process.Start(process))
            {
                proc.WaitForExit();
                Console.WriteLine("Exit code = " + proc.ExitCode);
                return proc.ExitCode == 0 ? true : false;
            }
        }

        public static bool joinDomain()
        {
            DomainInfo info = Config.get().getDomainInfo();
            return false;
        }

        public static bool startReboot()
        {
            return false;
        }

        public static bool installApp(Application app)
        {
            return false;
        }
    }
}
