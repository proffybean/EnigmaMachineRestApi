using static Enigma.Constants;
using static Enigma.Enums.Enumerations;
using Enigma.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma
{
    public class EnigmaMachine : IEnigmaMachine
    {
        private Plugboard plugboard;
        private Rotor rotor3;
        private Rotor rotor2;
        private Rotor rotor1;
        private Reflector reflector;
        private bool _leaveWhiteSpace;

        public EnigmaMachine()
        {
            plugboard = new Plugboard();
            reflector = new Reflector();
        }

        /// <summary>
        /// Chooses the rotors for your enigma machines
        /// </summary>
        public void ChooseRotors(RotorNumber rotor1, RotorNumber rotor2, RotorNumber rotor3)
        {
            switch (rotor3)
            {
                case RotorNumber.Rotor1:
                    this.rotor3 = new Rotor(rotorI, true, rotorITurnover);
                    break;
                case RotorNumber.Rotor2:
                    this.rotor3 = new Rotor(rotorII, true, rotorIITurnover);
                    break;
                case RotorNumber.Rotor3:
                    this.rotor3 = new Rotor(rotorIII, true, rotorIIITurnover);
                    break;
                case RotorNumber.Rotor4:
                    this.rotor3 = new Rotor(rotorIV, true, rotorIVTurnover);
                    break;
                case RotorNumber.Rotor5:
                    this.rotor3 = new Rotor(rotorV, true, rotorVTurnover);
                    break;
                default:
                    throw new IndexOutOfRangeException($"{rotor3} is not a valid rotor");
            }

            switch (rotor2)
            {
                case RotorNumber.Rotor1:
                    this.rotor2 = new Rotor(rotorI, false, rotorITurnover);
                    break;
                case RotorNumber.Rotor2:
                    this.rotor2 = new Rotor(rotorII, false, rotorIITurnover);
                    break;
                case RotorNumber.Rotor3:
                    this.rotor2 = new Rotor(rotorIII, false, rotorIIITurnover);
                    break;
                case RotorNumber.Rotor4:
                    this.rotor2 = new Rotor(rotorIV, false, rotorIVTurnover);
                    break;
                case RotorNumber.Rotor5:
                    this.rotor2 = new Rotor(rotorV, false, rotorVTurnover);
                    break;
                default:
                    throw new IndexOutOfRangeException($"{rotor2} is not a valid rotor");
            }

            switch (rotor1)
            {
                case RotorNumber.Rotor1:
                    this.rotor1 = new Rotor(rotorI, false, rotorITurnover);
                    break;
                case RotorNumber.Rotor2:
                    this.rotor1 = new Rotor(rotorII, false, rotorIITurnover);
                    break;
                case RotorNumber.Rotor3:
                    this.rotor1 = new Rotor(rotorIII, false, rotorIIITurnover);
                    break;
                case RotorNumber.Rotor4:
                    this.rotor1 = new Rotor(rotorIV, false, rotorIVTurnover);
                    break;
                case RotorNumber.Rotor5:
                    this.rotor1 = new Rotor(rotorV, false, rotorVTurnover);
                    break;
                default:
                    throw new IndexOutOfRangeException($"{rotor1} is not a valid rotor");
            }

            this.rotor3.AdvanceAdjacentRotor += this.rotor2.RotateHandler;
            this.rotor2.AdvanceAdjacentRotor += this.rotor1.RotateHandler;
        }

        /// <summary>
        /// Encodes one letter through the enigma machine
        /// </summary>
        public char Encode(char c)
        {
            return ConvertCharacter(c);
        }

        /// <summary>
        /// Encodes an entire string through the enigma machine
        /// </summary>
        public string Encode(string s)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in s.ToLower().ToCharArray())
            {
                if (Char.IsWhiteSpace(c) && _leaveWhiteSpace)
                {
                    sb.Append(c);
                    continue;
                }
                else if (!Char.IsLetter(c))
                {
                    continue;
                }
                else
                {
                    sb.Append(ConvertCharacter(c));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Inserts a wire from letter a to letter b
        /// </summary>
        public void SetPlugboardPair(char a, char b)
        {
            plugboard.SetWiring(a, b);
        }

        /// <summary>
        /// Wets all the wire pairs in the plugboard from pairs collection
        /// </summary>
        /// <param name="pairs"></param>
        public void SetPlugboard(IEnumerable<KeyValuePair<char, char>> pairs)
        {
            if (pairs != null)
            {
                foreach (KeyValuePair<char, char> pair in pairs)
                {
                    SetPlugboardPair(pair.Key, pair.Value);
                }
            }
        }

        /// <summary>
        /// Sets the dial on the given rotor
        /// </summary>
        public void SetRotorDial(int rotorNumber, char rotorSetting)
        {
            switch (rotorNumber)
            {
                case 1:
                    rotor1.SetDial(rotorSetting);
                    break;
                case 2:
                    rotor2.SetDial(rotorSetting);
                    break;
                case 3:
                    rotor3.SetDial(rotorSetting);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets the rotor dials for all three rotors
        /// </summary>
        public void SetRotorDials(char rotor1, char rotor2, char rotor3)
        {
            SetRotorDial(1, rotor1);
            SetRotorDial(2, rotor2);
            SetRotorDial(3, rotor3);
        }

        /// <summary>
        /// The entire round trip encoing process for the given letter
        /// </summary>
        public char ConvertCharacter(char c)
        {
            char next = plugboard.ConvertLetter(c);

            char next2 = rotor3.ConvertLetter(next);
            int rotor2index = rotor3.GetNextRotorsIndex(next2);

            char next3 = rotor2.ConvertLetter(rotor2index);
            int rotor1index = rotor2.GetNextRotorsIndex(next3);

            char next4 = rotor1.ConvertLetter(rotor1index);
            int reflectorIndex = rotor1.GetNextRotorsIndex(next4);

            char next5 = reflector.ReflectLetter(reflectorIndex);
            int nextRotor1Index = reflector.GetNextRotorsIndex(next5);

            char initLetter1 = rotor1.ReverseConvertLetter(nextRotor1Index);
            int nextRotor2Index = rotor1.ReverseGetNextRotorsIndex(initLetter1);

            char initLetter2 = rotor2.ReverseConvertLetter(nextRotor2Index);
            int nextRotor3Index = rotor2.ReverseGetNextRotorsIndex(initLetter2);

            char initLetter3 = rotor3.ReverseConvertLetter(nextRotor3Index);
            int plugBoardIndex = rotor3.ReverseGetNextRotorsIndex(initLetter3);

            char lightBoardChar = plugboard.ConvertLetter(plugBoardIndex);

            return lightBoardChar;
        }

        /// <summary>
        /// Conserve the plain text white space
        /// </summary>
        /// <param name="leaveWhiteSpace"></param>
        public void LeaveWhiteSpace(bool leaveWhiteSpace)
        {
            _leaveWhiteSpace = leaveWhiteSpace;
        }
    }
}
