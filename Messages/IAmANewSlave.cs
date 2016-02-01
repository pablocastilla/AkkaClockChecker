using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace Messages
{
    public class IAmANewSlave
    {
        public IActorRef SlaveRef { get; set; }

        public string Machine { get; set; }
    }
}
