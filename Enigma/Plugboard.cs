using Enigma.Extensions;
using Enigma.Interfaces;
using System;
using System.Collections.Generic;

namespace Enigma
{
    public class Plugboard : IPlugboard
    {
        const int TotalNumberOfPairs = 20;
        private int _numberPairs;
        private Dictionary<char, char> _wiring;

        public Plugboard()
        {
            NumberPairs = 0;
            _wiring = new Dictionary<char, char>();
        }

        public int NumberPairs
        {
            get { return _wiring.Count; }
            set
            {
                if (_numberPairs >= TotalNumberOfPairs)
                {
                    throw new ArgumentOutOfRangeException($"Can only set {TotalNumberOfPairs}");
                }

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Cannot set NumberPairs < 0");
                }
                _numberPairs = value;
            }
        }

        public char ConvertLetter(char c)
        {
            char letter;

            if (_wiring.ContainsKey(c))
            {
                letter = _wiring[c];
            }
            else
            {
                letter = c;
            }

            return letter;
        }

        public char ConvertLetter(int plugboardLetterLocation)
        {
            char letter;
            char initletter = plugboardLetterLocation.ConvertLocationToAscii();

            if (_wiring.ContainsValue(initletter))
            {
                letter = _wiring[initletter];
            }
            else
            {
                letter = initletter;
            }

            return letter;
        }

        public Dictionary<char, char> GetWiring()
        {
            var wiring = new Dictionary<char, char>();
            wiring = _wiring;
            return wiring;
        }

        public void SetWiring(char char1, char char2)
        {
            if (_wiring.TryGetValue(char1, out char outChar))
            {
                RemoveWiring(char1, outChar);
            }

            if (_wiring.TryGetValue(outChar, out char outChar2))
            {
                RemoveWiring(outChar, outChar2);
            }

            if (NumberPairs >= TotalNumberOfPairs)
            {
                throw new ArgumentOutOfRangeException($"Cannot set NumberPairs > 20");
            }

            _wiring.Add(char1, char2);
            _wiring.Add(char2, char1);
        }

        public void RemoveWiring(char char1, char char2)
        {
            if (_wiring.TryGetValue(char1, out char outChar))
            {
                if (outChar == char2)
                {
                    _wiring.Remove(char1);
                }
                    
            }

            if (_wiring.TryGetValue(char2, out outChar))
            {
                if (outChar == char1)
                { 
                    _wiring.Remove(char2);
                }
            }
        }

        public void ResetWiring()
        {
            _wiring.Clear();
            _numberPairs = 0;
        }
    }
}
