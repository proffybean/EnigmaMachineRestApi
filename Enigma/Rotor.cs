using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Interfaces;
using Enigma.Extensions;

namespace Enigma
{
    public delegate void AdvanceHandler(object sender, AdvanceEventArgs e);

    /// <summary>
    /// Rotor wiring found from https://en.wikipedia.org/wiki/Enigma_rotor_details
    /// </summary>
    public class Rotor : IRotor
    {
        private Dictionary<char, char> _wiring2;
        public event AdvanceHandler AdvanceAdjacentRotor;
        private char[] _wiring;
        private int _dialOffset;
        private bool _rotate;
        private int _adjacentRotorAdvanceOffset;

        public Rotor(string rotorMapping, bool rotate, int AdjacentRotorAdvanceOffset)
        {
            InitWiring(rotorMapping);
            InitWiring2(rotorMapping);
            _rotate = rotate;
            _adjacentRotorAdvanceOffset = AdjacentRotorAdvanceOffset;
        }

        public Rotor() : this(Constants.rotorIII, false, 0) { }

        public int Offset
        {
            get { return _dialOffset; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _dialOffset = value % 26;
            }
        }

        public char DialSetting
        {
            get { return (char)(Offset + Convert.ToByte('A')); }
        }

        public char ConvertLetter(char c) // TODO: make lowercase c.ToLower();
        {
            //ASCIIEncoding.ASCII.GetBytes(c);
            //int i = Encoding.ASCII.GetBytes(c);
            int i = c.GetLetterIndex();
            return ConvertLetter(i);
        }

        public char ConvertLetter(int i)
        {
            if (_rotate) { Rotate(); }
            //int index = (i + (26 - Offset)) % 26;
            int index = (i + Offset) % 26;
            char letter = _wiring[index];
            return letter;
        }

        // TODO: this function is wrong.
        //public char ReverseConvertLetter(char c)
        //{
        //    int i = Array.IndexOf(_wiring, c);
        //    //char convertedChar = (char)(i + Offset + Convert.ToByte('a'));
        //    return ReverseConvertLetter(i);
        //}

        public char ReverseConvertLetter(int i)
        {
            int characterNumber = Convert.ToByte('a') + (i + Offset) % 26;
            char convertedChar = (char)characterNumber;  // give T

            int locOnWheel = Array.IndexOf(_wiring, convertedChar); // where is T
            char initialChar = (char)(Convert.ToByte('a') + locOnWheel);
            return initialChar;
        }

        public int GetNextRotorsIndex(char convertedChar)
        {
            //int i = Convert.ToByte(convertedChar) - Convert.ToByte('a');
            int position = convertedChar.GetLetterIndex();
            int index = (position + (26 - Offset)) % 26;

            return index;
        }

        /// <summary>
        /// To Be Used on return trip
        /// </summary>
        public int ReverseGetNextRotorsIndex(char initialChar)
        {
            //int position = Convert.ToByte(initialChar) - Convert.ToByte('a');
            int position = initialChar.GetLetterIndex();
            int location = (position + (26 - Offset)) % 26;

            return location;
        }

        public int GetDialOffset()
        {
            return this.Offset;
        }

        public void RotateHandler(object sender, AdvanceEventArgs e)
        {
            Rotate();
        }

        public void Rotate()
        {
            if (Offset == _adjacentRotorAdvanceOffset)
            {
                AdvanceAdjacentRotor?.Invoke(this, new AdvanceEventArgs { message = "I'm advancing", character = _wiring[22] });
            }
            Offset = (Offset + 1) % 26;
        }

        public void SetDial(int offSet)
        {
            this.Offset = offSet;
        }

        public void SetDial(char c)
        {
            //int i = Convert.ToByte(c) - Convert.ToByte('a');
            int position = c.GetLetterIndex();
            SetDial(position);
        }

        public char[] GetCurrentRotor()
        {
            char[] curRotor;
            curRotor = _wiring;

            return curRotor;
        }

        private void InitWiring2(string rotorMapping)
        {
            _wiring2 = new Dictionary<char, char>();

            int i = 0;
            foreach (char c in Enumerable.Range(97, 26))
            {
                _wiring2.Add((char)c, rotorMapping[i++]);
            }
        }

        private void InitWiring(string rotorMapping)
        {
            _wiring = new char[26];

            for (int i = 0; i < 26; i++)
            {
                _wiring[i] = rotorMapping[i];
            }
        }
    }

    public class AdvanceEventArgs : EventArgs
    {
        public string message;
        public char character;
    }
}
