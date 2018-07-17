using Enigma.Extensions;
using Enigma.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Enigma
{
    public class Reflector : IReflector
    {
        private Dictionary<char, char> _wiring;

        public Reflector()
        {
            _wiring = new Dictionary<char, char>();
            SetWiring();
        }

        /// <summary>
        /// Wiring from http://enigmaco.de/enigma/enigma.html
        /// </summary>
        private void SetWiring()
        {
            _wiring.Add('a', 'y');
            _wiring.Add('b', 'r');
            _wiring.Add('c', 'u');
            _wiring.Add('d', 'h');
            _wiring.Add('e', 'q');
            _wiring.Add('f', 's');
            _wiring.Add('g', 'l');
            _wiring.Add('h', 'd');
            _wiring.Add('i', 'p');
            _wiring.Add('j', 'x');
            _wiring.Add('k', 'n');
            _wiring.Add('l', 'g');
            _wiring.Add('m', 'o');
            _wiring.Add('n', 'k');
            _wiring.Add('o', 'm');
            _wiring.Add('p', 'i');
            _wiring.Add('q', 'e');
            _wiring.Add('r', 'b');
            _wiring.Add('s', 'f');
            _wiring.Add('t', 'z');
            _wiring.Add('u', 'c');
            _wiring.Add('v', 'w');
            _wiring.Add('w', 'v');
            _wiring.Add('x', 'j');
            _wiring.Add('y', 'a');
            _wiring.Add('z', 't');
        }

        public Dictionary<char, char> GetWiring()
        {
            var Wiring = new Dictionary<char, char>();
            Wiring = _wiring;

            return Wiring;
        }

        public char ReflectLetter(char c)
        {
            return _wiring[c];
        }

        public char ReflectLetter(int i)
        {
            char c = _wiring.ElementAt(i).Value;
            return c;
        }

        public int GetNextRotorsIndex(char c)
        {
            int index = c.GetLetterIndex();

            return index;
        }
    }
}
