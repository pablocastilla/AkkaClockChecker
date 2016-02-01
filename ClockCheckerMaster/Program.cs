using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ClockCheckerMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.SetServiceName("ClockCheckerMaster");


                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.Service<MasterActorSystem>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
