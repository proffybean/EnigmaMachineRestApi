using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EnigmaMachineRestApi.Models
{
    [DataContract]
    public class RotorDto
    {
        //[DataMember]
        //public string Mapping { get; set; }

        //[DataMember]
        //public bool Rotate { get; set; }

        [DataMember]
        public int RotorNum { get; set; }

        //[DataMember(Name = "Offset")]
        //public int AdjacentRotorAdvanceOffset { get; set; }

        [DataMember(Name = "Setting")]
        public char InitialDialSetting { get; set; }
    }
}