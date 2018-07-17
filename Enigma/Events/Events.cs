using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Events
{
    public class AdvanceEventArgs : EventArgs
    {
        public string message;
        public char character;
    }
}
