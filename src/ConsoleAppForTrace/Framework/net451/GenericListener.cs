using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppForTrace
{
    public class GenericListener : TraceListener
    {
        List<string> strings;
        StringBuilder partial;

        public GenericListener()
        {
            strings = new List<string>();
            partial = new StringBuilder();
        }

        public List<string> Strings
        {
            get { return strings; }
        }

        public int Find(string sToFind)
        {
            int count = 0;
            foreach (string s in strings)
                if (s.IndexOf(sToFind) != -1)
                    count++;
            return count;
        }

        public void Clear()
        {
            partial.Remove(0, partial.Length);
            strings.Clear();
        }

        public override void Write(string message)
        {
            partial.Append(message);
        }

        public override void WriteLine(string message)
        {
            Write(message);
            strings.Add(partial.ToString());
            partial.Remove(0, partial.Length);
        }

        public int CountLinesContaining(string text)
        {
            int count = 0;
            foreach (string s in strings)
                if (s.Contains(text)) count++;
            return count;
        }
    }
}
