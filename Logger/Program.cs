using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;


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

            Expression<Func<int>> exp1 = () => Math.Max(n1, n2);

            int result = logger.Execute<int>(exp1, out log);
            Console.WriteLine(log);
            Console.WriteLine(result);
        }
    }

    class Logger
    {
        public T Execute<T>(Expression<Func<T>> toExecute, out string log)
        {
            log = "To replace";
            return toExecute.Compile().Invoke();
        }
    }

    public class TraceExpressionVisitor : ExpressionVisitor
    {
        public int indent = 0;

        public override Expression Visit(Expression node)
        {
            if (node == null)
                return base.Visit(node);

            Console.WriteLine("{0}{1} - {2}", new String(' ', indent * 4),
                node.NodeType, node.GetType());

            indent++;
            Expression result = base.Visit(node);
            indent--;

            return result;
        }
    }
}



