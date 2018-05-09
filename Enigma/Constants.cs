using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    public static class Constants
    {
        public static string rotorI = "EKMFLGDQVZNTOWYHXUSPAIBRCJ".ToLower();
        public static int rotorITurnover = 16;

        public static string rotorII = "AJDKSIRUXBLHWTMCQGZNPYFVOE".ToLower();
        public static int rotorIITurnover = 4;

        public static string rotorIII = "BDFHJLCPRTXVZNYEIWGAKMUSQO".ToLower();
        public static int rotorIIITurnover = 21;

        public static string rotorIV = "ESOVPZJAYQUIRHXLNFTGKDCMWB".ToLower();
        public static int rotorIVTurnover = 9;

        public static string rotorV = "VZBRGITYUPSDNHLXAWMJQOFECK".ToLower();
        public static int rotorVTurnover = 25;
    }
}
