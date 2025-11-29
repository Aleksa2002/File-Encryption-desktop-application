using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZastitaInformacija.Core.Interfaces;

namespace ZastitaInformacija.Core.Algorithms.Crypto
{
    internal class Enigma : ICryptoAlgorithm
    {
        private Rotor[]? _rotors;
        private Reflector? _reflector;
        private Plugboard? _plugboard;

        public byte[] GenerateRandomKey(byte[]? seed = null)
        {
            var sb = new StringBuilder();
            Random rng;
            if (seed is null)
            {
                rng = new Random();
            }
            else
            {
                var hash = SHA256.HashData(seed);
                int intSeed = BitConverter.ToInt32(hash, 0);
                rng = new Random(intSeed);
            }

            // --- Rotors ---
            List<int> rotors = [.. Enumerable.Range(0, 8)
                                         .OrderBy(_ => rng.Next())
                                         .Take(3)];

            foreach (var rotor in rotors)
            {
                char position = (char)('A' + rng.Next(26));
                char ring = (char)('A' + rng.Next(26));
                sb.Append(rotor);
                sb.Append(position);
                sb.Append(ring);
            }

            // --- Reflector ---
            char[] reflectors = ['A', 'B', 'C'];
            sb.Append(reflectors[rng.Next(reflectors.Length)]);

            // --- Plugboard ---
            int pairCount = rng.Next(0, 11); 
            int totalLetters = pairCount * 2;

            if (totalLetters > 0)
            {
                var letters = Enumerable.Range(0, 26)
                                        .Select(i => (char)('A' + i))
                                        .OrderBy(_ => rng.Next())
                                        .Take(totalLetters)
                                        .ToList();

                foreach (var c in letters)
                    sb.Append(c);
            }

            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        public byte[] Encrypt(byte[] data, byte[] key)
        {

            InitializeFromKey(key);
            var output = "";
            foreach (char c in Encoding.ASCII.GetString(data))
            {
                if (char.IsAsciiLetter(c))
                {
                    StepRotors();
                    char letter = char.ToUpper(c);
                    letter = _plugboard.Substitute(letter);
                    // Forward through rotors
                    foreach (var rotor in _rotors.Reverse())
                        letter = rotor.Forward(letter);
                    // Reflector
                    letter = _reflector.Reflect(letter);
                    // Backward through rotors
                    foreach (var rotor in _rotors)
                        letter = rotor.Backward(letter);
                    letter = _plugboard.Substitute(letter);
                    output += letter;
                }
            }

            return Encoding.ASCII.GetBytes(output);
        }
        public byte[] Decrypt(byte[] data, byte[] key)
        {
            return Encrypt(data, key); // Enigma is symmetric, so decryption is the same as encryption
        }

        private void StepRotors()
        {
            // Double-stepping mehanizam
            if (_rotors[1].AtNotch())
            {
                _rotors[0].Step();
                _rotors[1].Step();
            }
            else if (_rotors[2].AtNotch())
            {
                _rotors[1].Step();
            }

            _rotors[2].Step();
        }

        private void InitializeFromKey(byte[] key)
        {
            string keyStr = Encoding.ASCII.GetString(key);

            // --- Rotors (3 × (digit + char + char)) ---
            _rotors = new Rotor[3];
            for (int i = 0; i < 3; i++)
            {
                int rotorNumber = keyStr[i * 3] - '0'; // e.g. '1' -> 1
                char rotorPos = keyStr[i * 3 + 1];      // e.g. 'A'
                char rotorRing = keyStr[i * 3 + 2];    // e.g. 'Z'

                _rotors[i] = AvailableRotors[rotorNumber];

                _rotors[i].SetPosition(rotorPos);  // A=0, B=1, ...
                _rotors[i].SetRingSetting(rotorRing);
            }

            // --- Reflector ---
            char reflChar = keyStr[9]; // posle 9 znakova rotora
            _reflector = AvailableReflectors.First(r => r.Name == reflChar.ToString());

            // --- Plugboard ---
            var pairs = new List<(char, char)>();
            string plugStr = keyStr[10..];

            for (int i = 0; i < plugStr.Length; i += 2)
            {
                char a = plugStr[i];
                char b = plugStr[i + 1];
                pairs.Add((a, b));
            }

             _plugboard = new Plugboard(pairs);
        }


        // Rotor definitions (8 rotors, some with 2 notches)
        public static Rotor[] AvailableRotors =>
        [
            new Rotor("I",   "EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q"),
            new Rotor("II",  "AJDKSIRUXBLHWTMCQGZNPYFVOE", "E"),
            new Rotor("III", "BDFHJLCPRTXVZNYEIWGAKMUSQO", "V"),
            new Rotor("IV",  "ESOVPZJAYQUIRHXLNFTGKDCMWB", "J"),
            new Rotor("V",   "VZBRGITYUPSDNHLXAWMJQOFECK", "Z"),
            new Rotor("VI",  "JPGVOUMFYQBENHZRDKASXLICTW", "ZM"), // 2 notches
            new Rotor("VII", "NZJHGRCXMYSWBOUFAIVLPEKQDT", "ZM"), // 2 notches
            new Rotor("VIII","FKQHTLXOCBJSPDZRAMEWNIUYGV", "ZM"), // 2 notches
        ];

        // Reflector B wiring (can be extended)
        public static Reflector[] AvailableReflectors =>
        [
            new Reflector("A","EJMZALYXVBWFCRQUONTSPIKHGD"),
            new Reflector("B","YRUHQSLDPXNGOKMIEBFZCWVJAT"),
            new Reflector("C", "FVPJIAOYEDRZXWGCTKUQSBNMHL"),
        ];
    }
}
