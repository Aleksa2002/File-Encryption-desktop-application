using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaInformacija.Core.Algorithms.Crypto
{
    internal class Plugboard
    {
        private readonly Dictionary<char, char> _map;

        public Plugboard(IEnumerable<(char, char)>? pairs = null)
        {
            _map = new Dictionary<char, char>();
            if (pairs != null)
            {
                foreach (var (a, b) in pairs)
                {
                    _map[a] = b;
                    _map[b] = a;
                }
            }
        }
        public char Substitute(char c)
        {
            return _map.TryGetValue(c, out var mapped) ? mapped : c;
        }
    }
}
