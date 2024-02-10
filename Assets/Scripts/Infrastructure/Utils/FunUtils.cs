using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public static class FunUtils
    {
        public static TOut Map<TIn, TOut>(this TIn obj, Func<TIn, TOut> func) => func.Invoke(obj);

        public static TIn Apply<TIn>(this TIn obj, Action<TIn> func)
        {
            func.Invoke(obj);
            return obj;
        }

        public static void ForEach<TIn>(this IEnumerable<TIn> en, Action<TIn> func)
        {
            foreach (var value in en)
                func.Invoke(value);
        }
        
        public static void ForEachWithIndex<TIn>(this IEnumerable<TIn> en, Action<int, TIn> func)
        {
            var index = 0;
            foreach (var value in en)
                func.Invoke(index++, value);
        }
    }
}