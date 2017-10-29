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
        Func<TSource, TDestination> mapFunction;
        internal Mapper(Func<TSource, TDestination> func)
        {
            mapFunction = func;
        }
        public TDestination Map(TSource source)
        {
            return mapFunction(source);
        }
    }

    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var source = Expression.Parameter(typeof(TSource), "source");
            var body = Expression.MemberInit(Expression.New(typeof(TDestination)),
                source.Type.GetProperties().Select(p => Expression.Bind(typeof(TDestination).GetProperty(p.Name), Expression.Property(source, p))));
            var expr = Expression.Lambda<Mapper<TSource, TDestination>>(body, source);
            return expr.Compile();
        }
    }
    public class Foo {

    }
    public class Bar {

    }

}
