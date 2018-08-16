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

        /// <summary>
        /// The current number of pairs connected on the plugboard
        /// </summary>
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

        /// <summary>
        /// Encodes one letter
        /// </summary>
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

        /// <summary>
        /// Encodes a location on the return trip frmo the static rotor, 
        /// or Eintrittswalze (ETW), through the plugboard
        /// </summary>
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

        /// <summary>
        /// Returns the plugboard wiring
        /// </summary>
        /// <returns></returns>
        public Dictionary<char, char> GetWiring()
        {
            var wiring = new Dictionary<char, char>();
            wiring = _wiring;
            return wiring;
        }

        /// <summary>
        /// Sets a wire from one letter to another
        /// </summary>
        public void SetWiring(char char1, char char2)
        {
            char outChar;
            char outChar2;

            if (_wiring.TryGetValue(char1, out outChar))
            {
                RemoveWiring(char1, outChar);
            }

            if (_wiring.TryGetValue(outChar, out outChar2))
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

        /// <summary>
        /// Removes a wire from the plugboard
        /// </summary>
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

        /// <summary>
        /// Removes all the wirings from the plugboard
        /// </summary>
        public void ResetWiring()
        {
            _wiring.Clear();
            _numberPairs = 0;
        }
    }
}
