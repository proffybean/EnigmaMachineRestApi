using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.Interfaces;

namespace Enigma
{
    public class Reflector : IReflector
    {
        private Dictionary<char, char> Wiring;

        public Reflector()
        {
            Wiring = new Dictionary<char, char>();
            this.SetWiring();
        }

        /// <summary>
        /// Notice, if a -> b, then b must -> a for decryption
        /// </summary>
        //private void SetWiring()
        //{
        //    Wiring.Add('a', 'q');
        //    Wiring.Add('b', 'y');
        //    Wiring.Add('c', 'h');
        //    Wiring.Add('d', 'o');
        //    Wiring.Add('e', 'g');
        //    Wiring.Add('f', 'n');
        //    Wiring.Add('g', 'e');
        //    Wiring.Add('h', 'c');
        //    Wiring.Add('i', 'v');
        //    Wiring.Add('j', 'p');
        //    Wiring.Add('k', 'u');
        //    Wiring.Add('l', 'z');
        //    Wiring.Add('m', 't');
        //    Wiring.Add('n', 'f');
        //    Wiring.Add('o', 'd');
        //    Wiring.Add('p', 'j');
        //    Wiring.Add('q', 'a');
        //    Wiring.Add('r', 'x');
        //    Wiring.Add('s', 'w');
        //    Wiring.Add('t', 'm');
        //    Wiring.Add('u', 'k');
        //    Wiring.Add('v', 'i');
        //    Wiring.Add('w', 's');
        //    Wiring.Add('x', 'r');
        //    Wiring.Add('y', 'b');
        //    Wiring.Add('z', 'l');
        //}

        /// <summary>
        /// Wiring from http://enigmaco.de/enigma/enigma.html
        /// </summary>
        private void SetWiring()
        {
            Wiring.Add('a', 'y');
            Wiring.Add('b', 'r');
            Wiring.Add('c', 'u');
            Wiring.Add('d', 'h');
            Wiring.Add('e', 'q');
            Wiring.Add('f', 's');
            Wiring.Add('g', 'l');
            Wiring.Add('h', 'd');
            Wiring.Add('i', 'p');
            Wiring.Add('j', 'x');
            Wiring.Add('k', 'n');
            Wiring.Add('l', 'g');
            Wiring.Add('m', 'o');
            Wiring.Add('n', 'k');
            Wiring.Add('o', 'm');
            Wiring.Add('p', 'i');
            Wiring.Add('q', 'e');
            Wiring.Add('r', 'b');
            Wiring.Add('s', 'f');
            Wiring.Add('t', 'z');
            Wiring.Add('u', 'c');
            Wiring.Add('v', 'w');
            Wiring.Add('w', 'v');
            Wiring.Add('x', 'j');
            Wiring.Add('y', 'a');
            Wiring.Add('z', 't');
        }

        public Dictionary<char, char> GetWiring()
        {
            var Wiring = new Dictionary<char, char>();
            Wiring = this.Wiring;

            return Wiring;
        }

        public char ReflectLetter(char c)
        {
            return Wiring[c];
        }

        public char ReflectLetter(int i)
        {
            char c = Wiring.ElementAt(i).Value;
            return c;
        }

        public int GetNextRotorsIndex(char c)
        {
            int index = Convert.ToByte(c) - Convert.ToByte('a');

            return index;
        }
    }
}
