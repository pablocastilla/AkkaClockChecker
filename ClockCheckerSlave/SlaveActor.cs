using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Messages;
using Common;

namespace ClockCheckerSlave
{
    public class SlaveActor : ReceiveActor
    {
        string machine;

        public SlaveActor(string machine)
        {
            this.machine = machine;
           
            var master = Context.ActorSelection("akka.tcp://clockchecker@" + Common.Properties.Settings.Default.MasterMachineName+ ":8091/user/master");

            master.Tell(new IAmANewSlave() { SlaveRef = this.Self,Machine=machine });

            Receive<CheckClock>(s =>
            {
                var ok = true;
                var myDateTime = DateTime.Now;
                var myPreciseDateTime = PreciseDatetime.Now;
                var error= "";

                if (s.MyNow > myDateTime)
                {
                    ok = false;
                    error += " DateTime failed (" + (myDateTime - s.MyNow) + ") \n";
                }

                if (s.MyPreciseNow > myPreciseDateTime)
                {
                    ok = false;
                    error += " PreciseDateTime failed (" + (myPreciseDateTime - s.MyPreciseNow) + ") \n";
                }

                master.Tell(new CheckClockResponse()
                {
                    Error = error,
                    DateTimeReceived = s.MyNow,
                    PreciseDateTimeReceived = s.MyPreciseNow,
                    MyDateTime=myDateTime,
                    MyPreciseDateTime=myPreciseDateTime,
                    TimeOK=ok,
                    Machine=machine
                });

            });
        }
    }
}
