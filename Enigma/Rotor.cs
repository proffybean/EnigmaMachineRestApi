using Enigma.Events;
using Enigma.Extensions;
using Enigma.Interfaces;
using System;

namespace Enigma
{
    public delegate void AdvanceHandler(object sender, AdvanceEventArgs e);

    /// <summary>
    /// Rotor wiring found from https://en.wikipedia.org/wiki/Enigma_rotor_details
    /// </summary>
    public class Rotor : IRotor
    {
        public event AdvanceHandler AdvanceAdjacentRotor;
        private char[] _wiring;
        private int _dialOffset;
        private readonly bool _rotate;
        private readonly int _adjacentRotorAdvanceOffset;

        public Rotor(string rotorMapping, bool rotate, int AdjacentRotorAdvanceOffset)
        {
            InitializeWiring(rotorMapping);
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
            get { return (char)(Offset + 'A'.GetAsciiValue()); }
        }

        public char ConvertLetter(char c) // TODO: make lowercase c.ToLower();
        {
            int i = c.GetLetterIndex();
            return ConvertLetter(i);
        }

        public char ConvertLetter(int i)
        {
            if (_rotate)
            {
                Rotate();
            }

            int index = (i + Offset) % 26;
            char letter = _wiring[index];
            return letter;
        }

        public char ReverseConvertLetter(int i)
        {
            int characterNumber = Convert.ToByte('a') + (i + Offset) % 26;
            char convertedChar = (char)characterNumber;

            int locOnWheel = Array.IndexOf(_wiring, convertedChar);
            char initialChar = (char)(Convert.ToByte('a') + locOnWheel);
            return initialChar;
        }

        public int GetNextRotorsIndex(char convertedChar)
        {
            int position = convertedChar.GetLetterIndex();
            int index = (position + (26 - Offset)) % 26;

            return index;
        }

        /// <summary>
        /// To Be Used on return trip
        /// </summary>
        public int ReverseGetNextRotorsIndex(char initialChar)
        {
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
                AdvanceAdjacentRotor?.Invoke(this, 
                    new AdvanceEventArgs { message = "Rotor Advancing from setting", character = DialSetting });
            }
            Offset = (Offset + 1) % 26;
        }

        public void SetDial(int offSet)
        {
            this.Offset = offSet;
        }

        public void SetDial(char c)
        {
            int position = c.GetLetterIndex();
            SetDial(position);
        }

        public char[] GetCurrentRotorWiring()
        {
            char[] curRotor;
            curRotor = _wiring;

            return curRotor;
        }

        private void InitializeWiring(string rotorMapping)
        {
            _wiring = new char[26];

            for (int i = 0; i < 26; i++)
            {
                _wiring[i] = rotorMapping[i];
            }
        }
    }
}
