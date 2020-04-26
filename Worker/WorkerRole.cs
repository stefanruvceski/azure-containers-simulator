using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Worker
{
    public class WorkerRole : IWorkerRole
    {
        int ID;
        string port;
        ServiceHost sh;
        public void Start(string containerId,string port)
        {
            this.port = port;
            ID = int.Parse(containerId);
             Begin();
            //OpenServiceHost();
            Stop();
        }

        public void Stop()
        {
            Console.WriteLine("_______________________________________________\nWorkerRole Dll is Stoped.");
        }

        private void OpenServiceHost()
        {
            sh = new ServiceHost(typeof(Klasa));
            sh.AddServiceEndpoint(typeof(IInterface), new NetTcpBinding(), $"net.tcp://localhost:{port}/IInterface");
            sh.Open();
            Console.WriteLine($"Service host open on container {ID} port of IInteface servicehost is {port}");
            Console.ReadKey();
            sh.Close();
        }

        private void Begin()
        {
            while (true)
            {
                Console.WriteLine($"Working on container with port {ID}, my port for service host is {port}...");
            
                Thread.Sleep(1000);
            }
        }

        private void calculate()
        {
            Console.WriteLine("CALCULATOR");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Enter first number");
            int first = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter second number");
            int second = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter operation(ADD,SUB,DIV,MUL)");
            string op = Console.ReadLine();

            switch (op.ToUpper())
            {
                case "ADD":
                    Console.WriteLine($"{first} + {second} = {first + second}");
                    break;
                case "SUB":
                    Console.WriteLine($"{first} - {second} = {first - second}");
                    break;
                case "MUL":
                    Console.WriteLine($"{first} * {second} = {first * second}");
                    break;
                case "DIV":
                    Console.WriteLine($"{first} / {second} = {(double)((double)first / (double)second)}");
                    break;
                default:
                    break;
            }
        }
    }
}
