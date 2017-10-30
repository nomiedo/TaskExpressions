
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ExtensionMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            // var res = mapper.Map(new Foo());
            var res = mapper.Map(new Foo());
            Console.Read();
        }
    }

    public class Mapper<TSource, TDestination>
    {
        readonly Func<TSource, TDestination> _mapFunction;

        public Mapper(Func<TSource, TDestination> func)
        {
            _mapFunction = func;
        }

        public TDestination Map(TSource source)
        {
            return _mapFunction(source);
        }
    }

    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var source = Expression.Parameter(typeof(TSource), "source");
            var body = Expression.MemberInit(Expression.New(typeof(TDestination)),
                source.Type.GetProperties().Select(p => Expression.Bind(typeof(TDestination).GetProperty(p.Name), Expression.Property(source, p))));
            var expr = Expression.Lambda<Func<TSource, TDestination>>(body, source);
            var func = expr.Compile();

            return new Mapper<TSource, TDestination>(func);
        }
    }
    public class Foo {

    }
    public class Bar {

    }

}
