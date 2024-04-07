using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobileModelDTO.Model
{
    public class LogEntry
    {
        public string IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public uint IpAddressUint { get; set; }

    }
}
