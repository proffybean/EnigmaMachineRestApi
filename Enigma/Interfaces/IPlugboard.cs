using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Interfaces
{
    public interface IPlugboard
    {
        Dictionary<char, char> GetWiring();
        void SetWiring(char char1, char char2);
        void RemoveWiring(char char1, char char2);
        int NumberPairs { get; set; }
        char ConvertLetter(char c);
    }
}
