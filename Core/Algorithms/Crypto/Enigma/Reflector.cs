using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaInformacija.Core.Algorithms.Crypto
{
    internal class Reflector
    {
        private readonly string _name;
        private readonly string _wiring;

        public string Name => _name;
        public Reflector(string name, string wiring)
        {
            _name = name;
            _wiring = wiring;
        }
        public char Reflect(char c)
        {
            return _wiring[c - 'A'];
        }
    }
}
