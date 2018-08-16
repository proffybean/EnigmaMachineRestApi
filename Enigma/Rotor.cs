using Enigma.Events;
using Enigma.Extensions;
using Enigma.Interfaces;
using System;

namespace Enigma
{
    public delegate void AdvanceHandler(object sender, AdvanceEventArgs e);

    /// <summary>
    /// Rotor wiring found from https://en.wikipedia.org/wiki/Enigma_rotor_details
    /// 
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

        /// <summary>
        /// By default, set the rotor to rotor #3, don't rotate, and advance on letter 'A'
        /// </summary>
        public Rotor() : this(Constants.rotorIII, false, 0) { }

        /// <summary>
        /// Represents the offset, or the amount the rotor has turned. 
        /// A=0, B=1, C=2
        /// </summary>
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

        /// <summary>
        /// Returns the current setting on the dial
        /// </summary>
        public char DialSetting
        {
            get { return (char)(Offset + 'A'.GetAsciiValue()); }
        }

        /// <summary>
        /// Encodes one letter through the rotor.  This only works for the initial trip
        /// to the reflector
        /// </summary>
        public char ConvertLetter(char c)
        {
            int i = c.GetLetterIndex();
            return ConvertLetter(i);
        }

        /// <summary>
        /// Encodes one letter (in this case, the index on the rotor) through the rotor.  
        /// This only works for the initial trip to the reflector
        /// </summary>
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

        /// <summary>
        /// Encodes one letter (or the index on the rotor of the letter)
        /// through the rotor on the return trip
        /// </summary>
        public char ReverseConvertLetter(int i)
        {
            int characterNumber = Convert.ToByte('a') + (i + Offset) % 26;
            char convertedChar = (char)characterNumber;

            int locOnWheel = Array.IndexOf(_wiring, convertedChar);
            char initialChar = (char)(Convert.ToByte('a') + locOnWheel);
            return initialChar;
        }

        /// <summary>
        /// Encodes one letter through the rotor on the return trip
        /// </summary>
        public int GetNextRotorsIndex(char convertedChar)
        {
            int position = convertedChar.GetLetterIndex();
            int index = (position + (26 - Offset)) % 26;

            return index;
        }

        /// <summary>
        /// Returns the index of the encoded character. To be used when
        /// passing the letter to the next rotor
        /// </summary>
        public int ReverseGetNextRotorsIndex(char initialChar)
        {
            int position = initialChar.GetLetterIndex();
            int location = (position + (26 - Offset)) % 26;

            return location;
        }

        /// <summary>
        /// Returns the dial offset, or how many positions the rotor has been turned
        /// </summary>
        public int GetDialOffset()
        {
            return this.Offset;
        }

        /// <summary>
        /// Handles the rotos rotation
        /// </summary>
        public void RotateHandler(object sender, AdvanceEventArgs e)
        {
            Rotate();
        }

        /// <summary>
        /// Rotates the rotor. If necessary, also rotates the adjacent rotor.
        /// </summary>
        public void Rotate()
        {
            if (Offset == _adjacentRotorAdvanceOffset)
            {
                AdvanceAdjacentRotor?.Invoke(this, 
                    new AdvanceEventArgs { message = "Rotor Advancing from setting", character = DialSetting });
            }
            Offset = (Offset + 1) % 26;
        }

        /// <summary>
        /// Sets the dial on the rotor.
        /// </summary>
        public void SetDial(int offSet)
        {
            this.Offset = offSet;
        }

        /// <summary>
        /// Sets the dial on the rotor.
        /// </summary>
        public void SetDial(char c)
        {
            int position = c.GetLetterIndex();
            SetDial(position);
        }

        /// <summary>
        /// Returns the rotor wiring
        /// </summary>
        public char[] GetCurrentRotorWiring()
        {
            char[] curRotor;
            curRotor = _wiring;

            return curRotor;
        }

        /// <summary>
        /// Initializes the rotor wiring to the given string of characters
        /// </summary>
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
