using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compute
{
    public class RoleEnvironment : IRoleEnvironment
    {
        
        #region parameters
        public struct Packet
        {
            public string xml;
            public string dll;
        };
        public struct container
        {
            public bool state;
            public Packet packet;
            public string ClientPort;
        };

        public static Dictionary<int, container> containers = new Dictionary<int, container>() { { 1, new container() { ClientPort = "-1"} }, { 2, new container() { ClientPort = "-1" } }, { 3, new container() { ClientPort = "-1" } }, { 4, new container() { ClientPort = "-1" } } };
        #endregion parameters
        public string[] BrotherInstances(string myAssemblyName, string myAddress)
        {
            List<string> retVal = new List<string>();
            
            foreach (KeyValuePair<int, container> c in containers)
            {
                if (!string.IsNullOrEmpty(c.Value.packet.dll) && !string.IsNullOrEmpty(myAssemblyName))
                {
                    string[] string1 = c.Value.packet.dll.Split('\\');
                    string[] string2 = myAssemblyName.Split('\\');
                    if (string1[string1.Length - 1].Equals(string2[string2.Length - 1]) && !myAddress.Equals(c.Value.ClientPort) && !string.IsNullOrEmpty(c.Value.ClientPort))
                        retVal.Add(c.Value.ClientPort);
                }
            }

            return retVal.ToArray();
        }

        public string GetAddress(String myAssemblyName, string containerId)
        {
            if (Program.lastportfail == -1)
            {

                container c = containers[int.Parse(containerId)];
                c.ClientPort = $"200{containerId}0";
                Program.niz[int.Parse(containerId) - 1].ClientPort = $"200{containerId}0";
                containers[int.Parse(containerId)] = c;
                return containers[int.Parse(containerId)].ClientPort;
            }
            else
            {
                container c = containers[int.Parse(containerId)];
                c.ClientPort = Program.lastportfail.ToString();
                containers[int.Parse(containerId)] = c;
                Program.niz[int.Parse(containerId) - 1].ClientPort = c.ClientPort;
                Program.lastportfail = -1;
                return containers[int.Parse(containerId)].ClientPort;
                
            }
        }
    }
}
