using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EnigmaMachineRestApi.Models
{
    [DataContract]
    public class EnigmaMachineDto
    {
        [DataMember(Name = "MachineName")]
        public string MachineName { get; set; }

        [DataMember]
        public RotorDto Rotor1 { get; set; }

        [DataMember]
        public RotorDto Rotor2 { get; set; }

        [DataMember]
        public RotorDto Rotor3 { get; set; }

        [DataMember(Name = "Plugboard")]
        public PlugboardDto Plugboard { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}