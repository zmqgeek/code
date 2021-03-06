    using System;
    using System.Linq.Expressions;
    
    class Test
    {
        static void Main(string[] args)
        {
            short left = short.MaxValue;
            short right = 1;
            var a = Expression.Constant(left);
            var b = Expression.Constant(right);
            var sum = Expression.Add(a, b);
            var convert = Expression.ConvertChecked(sum, typeof(short));
            var convertLambda = Expression.Lambda<Func<short>>(convert);
            var convertFunc = convertLambda.Compile();
            Console.WriteLine("Conversion: {0}", convertFunc());
            var sumLambda = Expression.Lambda<Func<short>>(sum);
            var sumFunc = sumLambda.Compile();
            Console.WriteLine("Sum: {0}", sumFunc());
        }
    }
