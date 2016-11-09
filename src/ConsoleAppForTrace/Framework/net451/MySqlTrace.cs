using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppForTrace
{
    public class MySqlTrace
    {
        public static TraceSource source = new TraceSource("mysql");

        static MySqlTrace()
        {
            foreach (TraceListener listener in source.Listeners.Cast<TraceListener>().Where(listener => listener.GetType().ToString().Contains("MySql.EMTrace.EMTraceListener")))
            {
                QueryAnalysisEnabled = true;
                break;
            }
        }

        public static TraceListenerCollection Listeners { get; } = source.Listeners;

        public static SourceSwitch Switch
        {
            get { return source.Switch; }
            set { source.Switch = value; }
        }

        public static bool QueryAnalysisEnabled { get; set; }

        public static void EnableQueryAnalyzer(string host, int postInterval)
        {
            if (QueryAnalysisEnabled) return;
            // create a EMTraceListener and add it to our source
            TraceListener l = (TraceListener)Activator.CreateInstance(Type.GetType("MySql.EMTrace.EMTraceListener"), host, postInterval);

            if (l == null)
                throw new Exception("");

            source.Listeners.Add(l);
            Switch.Level = SourceLevels.All;
        }

        public static void DisableQueryAnalyzer()
        {
            QueryAnalysisEnabled = false;
            foreach (TraceListener l in from TraceListener l in Source.Listeners where l.GetType().ToString().Contains("EMTraceListener") select l)
            {
                source.Listeners.Remove(l);
                break;
            }
        }

        internal static TraceSource Source
        {
            get { return source; }
        }

        internal static void LogInformation(int id, string msg)
        {
            Source.TraceEvent(TraceEventType.Information, id, msg, MySqlTraceEventType.NonQuery, -1);
            Trace.TraceInformation(msg);
        }

        internal static void LogWarning(int id, string msg)
        {
            Source.TraceEvent(TraceEventType.Warning, id, msg, MySqlTraceEventType.NonQuery, -1);
            Trace.TraceWarning(msg);
        }

        internal static void LogError(int id, string msg)
        {
            Source.TraceEvent(TraceEventType.Error, id, msg, MySqlTraceEventType.NonQuery, -1);
            Trace.TraceError(msg);
        }

        public static void TraceEvent(TraceEventType eventType,
            MySqlTraceEventType mysqlEventType, string msgFormat, params object[] args)
        {
            Source.TraceEvent(eventType, (int)mysqlEventType, msgFormat, args);
        }
    }

    public enum UsageAdvisorWarningFlags
    {
        NoIndex = 1,
        BadIndex,
        SkippedRows,
        SkippedColumns,
        FieldConversion
    }

    public enum MySqlTraceEventType : int
    {
        ConnectionOpened = 1,
        ConnectionClosed,
        QueryOpened,
        ResultOpened,
        ResultClosed,
        QueryClosed,
        StatementPrepared,
        StatementExecuted,
        StatementClosed,
        NonQuery,
        UsageAdvisorWarning,
        Warning,
        Error,
        QueryNormalized
    }
}

