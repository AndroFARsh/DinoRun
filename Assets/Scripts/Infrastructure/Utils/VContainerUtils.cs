using System;
using System.Collections;
using System.Collections.Generic;
using VContainer;

namespace Infrastructure.Utils
{
    public static class VContainerUtils
    {
        public class ListParameter<TParam> : IReadOnlyList<Type>
        {
            private readonly List<Type> _value = new();

            public ListParameter<TParam> Register<T>() where T : TParam
            {
                _value.Add(typeof(T));
                return this;
            }

            public IEnumerator<Type> GetEnumerator() => _value.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _value.GetEnumerator();

            public int Count => _value.Count;

            public Type this[int index] => _value[index];
        } 
        
        public static ListParameter<TParam> WithReadOnlyListTypeParameter<TParam>(this RegistrationBuilder builder)
        {
            var listParameter = new ListParameter<TParam>();
            builder.WithParameter(typeof(IReadOnlyList<Type>), listParameter);
            return listParameter;
        }
        
        public static ListParameter<TParam> WithEnumerableTypeParameter<TParam>(this RegistrationBuilder builder)
        {
            var listParameter = new ListParameter<TParam>();
            builder.WithParameter(typeof(IEnumerable<Type>), listParameter);
            return listParameter;
        }
    }
}