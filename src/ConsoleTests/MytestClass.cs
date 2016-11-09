using ConsoleAppForTrace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleTests
{
    public class Mytestclass
    {
        [Fact]
        public void FirstTest()
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
            d.Close();


        }
    }
}
