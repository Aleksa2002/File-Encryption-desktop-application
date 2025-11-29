using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZastitaInformacija.Core.Algorithms.Crypto
{
    public class Rotor
    {
        private readonly string _name;
        private readonly string _wiring;
        private readonly HashSet<int> _notches;
        private int _position;
        private int _ringSetting;

        public string Name => _name;

        public Rotor(string name, string wiring, string notchLetters)
        {
            _wiring = wiring;
            _notches = [.. notchLetters.Select(c => c - 'A')];
            _position = 0;
            _ringSetting = 0;
            _name = name;
        }

        public void SetPosition(char positon)
        {
            _position = positon - 'A';
        }

        public void SetRingSetting(char ringSetting)
        {
            _ringSetting = ringSetting - 'A';
        }


        public void Step()
        {
            _position = (_position + 1) % 26;
        }

        public bool AtNotch()
        {
            return _notches.Contains(_position);
        }

        public char Forward(char c)
        {
            int input = (c - 'A' + _position - _ringSetting + 26) % 26;
            char wired = _wiring[input];
            int output = (wired - 'A' - _position + _ringSetting + 26) % 26;
            return (char)('A' + output);
        }

        public char Backward(char c)
        {
            int input = (c - 'A' + _position - _ringSetting + 26) % 26;
            int wired = _wiring.IndexOf((char)('A' + input));
            int output = (wired - _position + _ringSetting + 26) % 26;
            return (char)('A' + output);
        }
    }
}
