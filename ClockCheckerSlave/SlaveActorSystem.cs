using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Topshelf;

namespace ClockCheckerSlave
{
    public class SlaveActorSystem : ServiceControl
    {

        private ActorSystem actorSystem;
        private IActorRef slave;

        public bool Start(HostControl hostControl)
        {
            actorSystem = ActorSystem.Create("ClockChecker");

            slave = actorSystem.ActorOf(
                   Props.Create(() => new SlaveActor(Environment.MachineName)));
            

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            actorSystem.Terminate();
            return true;
        }
    }
}
