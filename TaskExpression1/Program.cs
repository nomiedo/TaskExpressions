using System;

namespace TaskExpression1
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var res = mapper.Map(new Foo());
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

        }
    }
    public class Foo
    {
        public string Word { get; set; }

        public int Number { get; set; }
    }
    public class Bar
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }

}
