using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class CheckClockResponse
    {
        public DateTime DateTimeReceived { get; set; }
        public DateTime MyDateTime { get; set; }

        public DateTime PreciseDateTimeReceived { get; set; }

        public DateTime MyPreciseDateTime { get; set; }

        public bool TimeOK { get; set; }

        public string Error { get; set; }

        public string Machine { get; set; }
    }
}
