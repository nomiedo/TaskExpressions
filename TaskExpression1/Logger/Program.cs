using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        static void Main()
        {
            string log;
            Logger logger = new Logger();
            int n1 = 30;
            int n2 = 50;
            int result = logger.Execute<int>(() => Math.Max(n1, n2), out log);
            Console.WriteLine(log);
            Console.WriteLine(result);
        }

        class Logger
        {
            public T Execute<T>(Func<T> toExecute, out string log)
            {
                log = "To replace";
                return toExecute();
            }

        }
    }
}



