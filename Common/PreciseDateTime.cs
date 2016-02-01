using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Common
{
    public class PreciseDatetime
    {

        // using DateTime.Now resulted in many many log events with the same timestamp.
        // use static variables in case there are many instances of this class in use in the same program
        // (that way they will all be in sync)
        private static readonly Stopwatch myStopwatch = new Stopwatch();
        private static System.DateTime myStopwatchStartTime;
        private static DateTime lastDateTime;
        public static Object o = new Object();

        static PreciseDatetime()
        {
            Reset();

            try
            {
                // In case the system clock gets updated
                SystemEvents.TimeChanged += SystemEvents_TimeChanged;
            }
            catch (Exception)
            {
            }
        }

        static void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            Reset();
        }

        // SystemEvents.TimeChanged can be slow to fire (3 secs), so allow forcing of reset
        static public void Reset()
        {
            myStopwatchStartTime = System.DateTime.Now;
            myStopwatch.Restart();
           
           
        }

        public static System.DateTime Now { 
            get {
                

                lock (o)
                {
                    myStopwatch.Restart();

                    var now = DateTime.Now;

                    if (lastDateTime >= now)
                    {
                        now = lastDateTime.AddTicks(100);
                    }                   
                    
                    var elapsed = myStopwatch.Elapsed;
                                                      
                    var result = now.Add(elapsed.Add(elapsed));

                    lastDateTime = result;

                    return result;
                }
            
            } 
        }
    }
}
