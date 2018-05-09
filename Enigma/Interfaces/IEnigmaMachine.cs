using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Interfaces
{
    public interface IEnigmaMachine
    {
        void SetPlugboardPair(char a, char b);
        void SetPlugboard(IEnumerable<KeyValuePair<char, char>> pairs);
        void ChooseRotors(int rotor1, int rotor2, int rotor3);
        void SetRotorDial(int rotorNumber, char rotorSetting);
        char Encode(char c);
        string Encode(string s);
    }
}
