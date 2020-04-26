using ServiceContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Container
{
    public class Container : IContainer
    {
        public string Load(string assemblyName)
        {
            Program.flag = 1;
            Program.assemblyName = assemblyName;
            string[] pom = assemblyName.Split('\\', '_');
            string port = Program.ContainerId;
            string portForClient;
            Console.WriteLine("Port of this container is "+ ( portForClient =  Program.proxy.GetAddress(assemblyName,Program.ContainerId)));
            Assembly DLL = Assembly.Load(File.ReadAllBytes(assemblyName));
            dynamic c = null;

            BrotherInstance(assemblyName, portForClient);

            for (int j = 0; j < DLL.GetExportedTypes().Length; j++)
            {
                string b = DLL.GetExportedTypes()[j].Name;

                if (b == "WorkerRole")
                {
                    c = Activator.CreateInstance(DLL.GetExportedTypes()[j]);
                    new Thread(() =>
                    {
                        //Thread.CurrentThread.IsBackground = true;
                        c.Start($"100{port}0",portForClient);
                        Thread.Sleep(1000);
                        end(assemblyName,port);
                    }).Start();
                }
            }
            return "success";
        }

        private static void BrotherInstance(string assemblyName,string port)
        {
            new Thread(() =>
            {
                // Thread.CurrentThread.IsBackground = true;
                while (true)
                {

                    foreach (string s in Program.proxy.BrotherInstances(assemblyName, port))
                    {
                        Console.WriteLine("Brother Instances->" + s);
                    }
                    Thread.Sleep(5000);
                }

            }).Start();
        }

        private static void end(string assemblyName,string port)
        {
            Program.flag = -1;
            Console.WriteLine("end");
            Console.Clear();
            Console.WriteLine("Service host is open on " + port + " port.\n_______________________________________________");
            Program.assemblyName = null;
            File.Delete(assemblyName);
        }

        public string CheckState()
        {
            if(Program.flag == -1)
            {
                return "free";
            }
            else
            {
                return "notfree";
            }
        }
    }
}
