using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EnigmaMachineRestApi.Models
{
    /// <summary>
    /// The RotorDto class contains info for each rotor including which rotor and the initial setting
    /// </summary>
    [DataContract]
    public class RotorDto
    {
        //[DataMember]
        //public string Mapping { get; set; }

        //[DataMember]
        //public bool Rotate { get; set; }

        /// <summary>
        /// This is the rotor number.  There are 5 rotors to choose from in this version.
        /// </summary>
        [DataMember]
        public int RotorNum { get; set; }

        //[DataMember(Name = "Offset")]
        //public int AdjacentRotorAdvanceOffset { get; set; }

        /// <summary>
        /// The initial setting on the dial.  Ex: 'A' or 'W'
        /// </summary>
        [DataMember(Name = "Setting")]
        public char InitialDialSetting { get; set; }
    }
}