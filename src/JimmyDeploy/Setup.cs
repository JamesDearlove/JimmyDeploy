using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using IWshRuntimeLibrary;
using JimmyDeploy.Data;
using Microsoft.Win32;

namespace JimmyDeploy
{
    public class Setup
    {
        static int step = 1;

        public static void start()
        {
            //new Thread(() =>
            //{
            //    foreach (Data.Task t in Config.get().tasks)
            //    {
            //        if (string.IsNullOrWhiteSpace(t.progress))
            //        {
            //            t.progress = "Running";
            //            bool result = false;
            //            switch (t.name)
            //            {
            //                case "Reboot":
            //                    result = startReboot();
            //                    if (result)
            //                    {
            //                        System.Windows.Application.Current.Shutdown();
            //                    }
            //                    break;
            //                case "Install App":
            //                    //result = installApp((Data.Application)t.taskObj);
            //                    break;
            //                case "Join Domain":
            //                    result = joinDomain();
            //                    break;
            //                case "Rename Computer":
            //                    result = changeComputerInfo();
            //                    break;
            //            }
            //            t.progress = result ? "Complete" : "Failed";
            //        }
            //        step++;
            //    }
             
            //    Console.WriteLine("Hello, world");
            //}).Start();
        }

        public static bool enableAutoLogin()
        {
            //DomainInfo domain = Config.get().getDomainInfo();
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

            //key.SetValue("AutoAdminLogon", 1, RegistryValueKind.String);
            //key.SetValue("DefaultDomainName", domain.Name, RegistryValueKind.String);
            //key.SetValue("DefaultUserName", domain.Username, RegistryValueKind.String);
            //key.SetValue("DefaultPassword", domain.Password, RegistryValueKind.String);
            key.Close();

            return true;
        }

        public static bool disableAutoLogin()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

            key.SetValue("AutoAdminLogon", 0, RegistryValueKind.String);
            key.SetValue("DefaultDomainName", "", RegistryValueKind.String);
            key.SetValue("DefaultUserName", "", RegistryValueKind.String);
            key.SetValue("DefaultPassword", "", RegistryValueKind.String);
            key.Close();

            return true;
        }

        public static void enableUAC()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
            key.SetValue("EnableLUA", 1);
            key.Close();
        }

        public static void removeStartup()
        {
            var startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);

            System.IO.File.Delete(Path.Combine(startupFolderPath, @"JimmyDeploy.lnk"));
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
            process.Arguments = "computersystem where caption='" + System.Environment.MachineName + "' rename " + Config.get().CompName;

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
            process.Arguments = "config server /srvcomment:\"" + Config.get().CompDesc + "\"";

            using (Process proc = Process.Start(process))
            {
                proc.WaitForExit();
                Console.WriteLine("Exit code = " + proc.ExitCode);
                return proc.ExitCode == 0 ? true : false;
            }
        }

        public static bool joinDomain()
        {
            //// https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/joindomainorworkgroup-method-in-class-win32-computersystem
            //// https://juanchif.wordpress.com/2009/12/09/joining-a-computer-to-a-domain-programatically-from-c/
            //int JOIN_DOMAIN = 1;
            //int ACCT_CREATE = 2;
            //int ACCT_DELETE = 4;
            //int WIN9X_UPGRADE = 16;
            //int DOMAIN_JOIN_IF_JOINED = 32;
            //int JOIN_UNSECURE = 64;
            //int MACHINE_PASSWORD_PASSED = 128;
            //int DEFERRED_SPN_SET = 256;
            //int INSTALL_INVOCATION = 262144;

            //DomainInfo info = Config.get().getDomainInfo();

            //int parameters = JOIN_DOMAIN | ACCT_CREATE | DOMAIN_JOIN_IF_JOINED;

            //object[] methodArgs = { info.Name, info.Password, info.Username + "@" + info.Name, null, parameters };

            //ManagementObject computerSystem = new ManagementObject("Win32_ComputerSystem.Name='" + Environment.MachineName + "'");
            //computerSystem.Scope.Options.Authentication = System.Management.AuthenticationLevel.PacketPrivacy;
            //computerSystem.Scope.Options.Impersonation = ImpersonationLevel.Impersonate;
            //computerSystem.Scope.Options.EnablePrivileges = true;

            //object Oresult = computerSystem.InvokeMethod("JoinDomainOrWorkgroup", methodArgs);

            //int result = (int)Convert.ToInt32(Oresult);

            //if (result == 0)
            //{
            //    Console.WriteLine("Joined Successfully!");
            //    return true;
            //}
            //// If here, domain joining has failed
            //MessageBox.Show("An error has occurred attempting to join the domain. \n Error: " + result.ToString(),
            //                "Domain Join Error",
            //                MessageBoxButton.OK, 
            //                MessageBoxImage.Error);
            return false;
        }

        public static bool startReboot()
        {
            if (!Config.get().AutoLogin)
            {
                enableAutoLogin();
            }
            enableStartup();

            //MessageBox.Show("Reboot hit");
            //return true;
            ProcessStartInfo process = new ProcessStartInfo();
            process.FileName = "shutdown";
            process.Arguments = "/r /f /t 0";

            using (Process proc = Process.Start(process))
            {
                proc.WaitForExit();
                Console.WriteLine("Exit code = " + proc.ExitCode);
                return proc.ExitCode == 0 ? true : false;
            }
        }

        public static bool enableStartup()
        {
            // https://bytescout.com/blog/create-shortcuts-in-c-and-vbnet.html
            step++;
            var args = step.ToString();

            var startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            var shell = new WshShell();
            var shortCutLinkFilePath = Path.Combine(startupFolderPath, @"JimmyDeploy.lnk");
            var windowsApplicationShortcut = (IWshShortcut)shell.CreateShortcut(shortCutLinkFilePath);
            windowsApplicationShortcut.Description = "How to create short for application example";
            windowsApplicationShortcut.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
            windowsApplicationShortcut.TargetPath = System.Windows.Forms.Application.ExecutablePath;
            windowsApplicationShortcut.Arguments = args;
            windowsApplicationShortcut.Save();
            return true;
        }

        //public static bool installApp(Data.Application app)
        //{
        //    ProcessStartInfo process = new ProcessStartInfo();
        //    process.FileName = app.filePath;
        //    process.Arguments = app.arguments;

        //    using (Process proc = Process.Start(process))
        //    {
        //        proc.WaitForExit();
        //        Console.WriteLine("Exit code = " + proc.ExitCode);
        //        return proc.ExitCode == 0 ? true : false;
        //    }
        //}
    }
}
