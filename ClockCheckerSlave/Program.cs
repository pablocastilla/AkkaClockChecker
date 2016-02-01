using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ClockCheckerSlave
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.SetServiceName("ClockCheckerSlave");


                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.Service<SlaveActorSystem>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
