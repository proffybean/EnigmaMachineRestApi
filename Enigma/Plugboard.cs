using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Enigma.Interfaces;

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

        public char ConvertLetter(int i)
        {
            // convert i to letter
            char letter;
            char initletter = (char)(Convert.ToByte('a') + i);

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
            char outChar; 

            if (_wiring.TryGetValue(char1, out outChar))
            {
                RemoveWiring(char1, outChar);
            }

            //if (_wiring.ContainsKey(char1))
            //{
            //    RemoveWiring(char1, char2);
            //}

            char outChar2;

            if (_wiring.TryGetValue(outChar, out outChar2))
            {
                RemoveWiring(outChar, outChar2);
            }

            if (NumberPairs >= TotalNumberOfPairs)
            {
                throw new ArgumentOutOfRangeException($"Cannot set NumberPairs > 20");
            }

            //NumberPairs++;
            _wiring.Add(char1, char2);
            _wiring.Add(char2, char1);
        }

        //public void RemoveWiring(char char1)
        //{
        //    if (!_wiring.ContainsKey(char1))
        //    {
        //        //throw new ArgumentException("That key doesn't exist");
        //    }

        //    char char2 = _wiring[char1];
        //    if (!_wiring.ContainsKey(char2))
        //    {
        //        //throw new ArgumentException("That key doesn't exist");
        //    }

        //    RemoveWiring(char1, char2);

        //    //if (!_wiring.ContainsKey(char1))
        //    //{
        //    //    throw new ArgumentException("That key doesn't exist");
        //    //}
        //    //NumberPairs--;
        //    //_wiring.Remove(char1);
        //}

        public void RemoveWiring(char char1, char char2)
        {
            char outChar;

            if (_wiring.TryGetValue(char1, out outChar))
            {
                if (outChar == char2)
                    _wiring.Remove(char1);
            }
            //_wiring.Remove(char1);
            //throw new ArgumentException("That key doesn't exist");


            if (_wiring.TryGetValue(char2, out outChar))
            {
                if (outChar == char1)
                    _wiring.Remove(char2);
                //throw new ArgumentException("That key doesn't exist");
            }
            //NumberPairs--;

            

            //_wiring.Remove(char1);
            //_wiring.Remove(char2);

            
        }

        public void ResetWiring()
        {
            _wiring.Clear();
            _numberPairs = 0;
        }
    }
}
