using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppForTrace
{
    public class MyDriver
    {
        private string _connected;
        public string Name { get; set; }
        public string Connected { 
          get {
                return _connected;
            } 
        }

        public MyDriver()
        {
            Name = "a new instance";
            _connected = string.Empty;
        }

        public void Connnect()
        {
          MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.ConnectionOpened,"TraceOpenConnection", 1, "a connection", 2);
          _connected = "true";
        }

        public void Close()
        {
            MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.ConnectionOpened, "TraceOpenConnection", 1, "a connection", 2);
            _connected = "false";
        }

    }
}
