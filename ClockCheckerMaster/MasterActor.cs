using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common;
using Messages;

namespace ClockCheckerMaster
{
    
    public class AttemptToStartJob
    {
    }

    public class MasterActor : ReceiveActor
    {
        public List<IActorRef> Slaves { get; set; }
        protected ICancelable JobStarter;
        int checks = 0;

        public MasterActor()
        {
            JobStarter = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromMilliseconds(1000),
                        TimeSpan.FromMilliseconds(1000), Self, new AttemptToStartJob(), Self);

            Slaves = new List<IActorRef>();

            Receive<AttemptToStartJob>(start =>
            {
                foreach (var s in Slaves)
                {
                    s.Tell(new CheckClock() { MyNow = DateTime.Now, MyPreciseNow = PreciseDatetime.Now });
                }

                checks++;
            });

            Receive<IAmANewSlave>(s =>
            {
                Slaves.Add(s.SlaveRef);

                Console.WriteLine("Slave from "+s.Machine+" added");

            });

            Receive<CheckClockResponse>(r =>
            {
                if (r.TimeOK)
                {
                    Console.WriteLine("Check "+checks+" ok from "+r.Machine);
                }
                else
                {
                    Console.WriteLine("Check " + checks + " no ok from("+r.Machine+"): " + r.Error);

                }

            });
        }
    }
}
