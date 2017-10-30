using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
            var logVisitor =  new LoggerVisitor();
            logVisitor.Visit(toExecute);
            log = logVisitor.log;

            return toExecute.Compile().Invoke();
        }
    }

    public class LoggerVisitor : ExpressionVisitor
    {
        public int indent = 0;
        public string log = string.Empty;
        protected override Expression VisitMember
            (MemberExpression member)
        {
            if (member.Expression is ConstantExpression &&
                member.Member is FieldInfo)
            {
                object container =
                    ((ConstantExpression)member.Expression).Value;
                object value = ((FieldInfo)member.Member).GetValue(container);
                var newMeber = $"{member.Member.Name} = {value}";
                Console.WriteLine(newMeber);
                log += Environment.NewLine + newMeber;
            }
            return base.VisitMember(member);
        }

    }
}



