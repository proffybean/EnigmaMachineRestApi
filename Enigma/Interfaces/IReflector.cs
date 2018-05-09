using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Interfaces
{
    public interface IReflector
    {
        char ReflectLetter(char c);
        Dictionary<char, char> GetWiring();
    }
}
