using ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    public class Klasa : IInterface
    {
        public string funkcija(string s)
        {
            Console.WriteLine(s);
            return s;
        }
    }
}
