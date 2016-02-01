using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Topshelf;

namespace ClockCheckerMaster
{
    public class MasterActorSystem : ServiceControl
    {

        private ActorSystem actorSystem;

        private IActorRef master;

        public bool Start(HostControl hostControl)
        {
            actorSystem = ActorSystem.Create("clockchecker");


            master = actorSystem.ActorOf(
                    Props.Create(() => new MasterActor()),"master");



            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            actorSystem.Terminate();
            return true;
        }
    }
}
