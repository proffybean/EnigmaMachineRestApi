using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
