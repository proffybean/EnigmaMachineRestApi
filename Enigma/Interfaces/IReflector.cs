using System.Collections.Generic;

namespace Enigma.Interfaces
{
    public interface IReflector
    {
        char ReflectLetter(char c);
        Dictionary<char, char> GetWiring();
    }
}
