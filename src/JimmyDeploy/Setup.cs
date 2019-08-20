using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
                            result = installApp((Data.Application)t.taskObj);
                            break;
                        case "Join Domain":
                            result = joinDomain();
                            break;
                        case "Rename Computer":
                            result = changeComputerInfo();
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
            //if (!changeComputerName())
            //{
            //    result = false;
            //}
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
            //MessageBox.Show("config server / srvcomment:\"" + Config.get().compDesc + "\" ");
            process.Arguments = "config server /srvcomment:\"" + Config.get().compDesc + "\"";

            using (Process proc = Process.Start(process))
            {
                proc.WaitForExit();
                Console.WriteLine("Exit code = " + proc.ExitCode);
                return proc.ExitCode == 0 ? true : false;
            }
        }

        public static bool joinDomain()
        {
            // https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/joindomainorworkgroup-method-in-class-win32-computersystem
            // https://juanchif.wordpress.com/2009/12/09/joining-a-computer-to-a-domain-programatically-from-c/
            int JOIN_DOMAIN = 1;
            int ACCT_CREATE = 2;
            int ACCT_DELETE = 4;
            int WIN9X_UPGRADE = 16;
            int DOMAIN_JOIN_IF_JOINED = 32;
            int JOIN_UNSECURE = 64;
            int MACHINE_PASSWORD_PASSED = 128;
            int DEFERRED_SPN_SET = 256;
            int INSTALL_INVOCATION = 262144;

            DomainInfo info = Config.get().getDomainInfo();

            int parameters = JOIN_DOMAIN | ACCT_CREATE | DOMAIN_JOIN_IF_JOINED;  

            MessageBox.Show(info.Username + "@" + info.Name);
            MessageBox.Show(info.Password);

            object[] methodArgs = { info.Name, info.Password, info.Username + "@" + info.Name, null, parameters };

            ManagementObject computerSystem = new ManagementObject("Win32_ComputerSystem.Name='" + Environment.MachineName + "'");
            computerSystem.Scope.Options.Authentication = System.Management.AuthenticationLevel.PacketPrivacy;
            computerSystem.Scope.Options.Impersonation = ImpersonationLevel.Impersonate;
            computerSystem.Scope.Options.EnablePrivileges = true;

            object Oresult = computerSystem.InvokeMethod("JoinDomainOrWorkgroup", methodArgs);

            int result = (int)Convert.ToInt32(Oresult);

            if (result == 0)
            {
                Console.WriteLine("Joined Successfully!");
                return true;
            }
            MessageBox.Show(result.ToString());
            //TODO: Add error codes here, 
            return false;
        }

        public static bool startReboot()
        {
            return false;
        }

        public static bool installApp(Data.Application app)
        {
            return false;
        }
    }
}
