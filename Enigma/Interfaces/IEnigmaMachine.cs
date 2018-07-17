using System.Collections.Generic;
using static Enigma.Enums.Enumerations;

namespace Enigma.Interfaces
{
    public interface IEnigmaMachine
    {
        void SetPlugboardPair(char a, char b);
        void SetPlugboard(IEnumerable<KeyValuePair<char, char>> pairs);
        void ChooseRotors(RotorNumber rotor1, RotorNumber rotor2, RotorNumber rotor3);
        void SetRotorDial(int rotorNumber, char rotorSetting);
        char Encode(char c);
        string Encode(string s);
    }
}
