using System;

namespace Enigma.Extensions
{
    public static class Extensions
    {
        public static char ConvertLocationToAscii(this int location)
        {
            char initletter = (char)(Convert.ToByte('a') + location);

            return initletter;
        }
        
        /// <summary>
        /// Returns the letter index, or location, on a rotor.
        /// </summary>
        public static int GetLetterIndex(this char c)
        {
            int index = Convert.ToByte(c) - Convert.ToByte('a');

            return index;
        }

        public static uint GetAsciiValue(this char c)
        {
            uint asciiValue = Convert.ToByte(c);

            return asciiValue;
        }
    }
}
