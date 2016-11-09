using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppForTrace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MySqlTrace.Listeners.Clear();
            MySqlTrace.Switch.Level = SourceLevels.All;
#if !NETCORE10
            GenericListener listener = new GenericListener();
            MySqlTrace.Listeners.Add(listener);
#endif
            var d = new MyDriver();
            if (!d.Connected.Equals("true"))
                d.Connnect();

            Console.WriteLine("Opened");
            d.Close();

#if !NETCORE10
            Console.Write("strings " + listener.Strings.Count);
#endif

            Console.ReadKey();
        }
    }
}
