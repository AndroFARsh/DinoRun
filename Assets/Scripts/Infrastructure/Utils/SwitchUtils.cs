using System;
using System.Collections.Generic;

namespace Infrastructure.Utils
{
    public static class Switch
    {
        public static TypeSwitch CreateTypeSwitch() => new();

        public static LambdaSwitch<T> CreateLambdaSwitch<T>() => new();

        public class LambdaSwitch<TKey>
        {
            private Action _default;
            private readonly Dictionary<TKey, Action> _matches = new();

            public LambdaSwitch<TKey> Case(TKey key, Action action)
            {
                _matches.Add(key, action);
                return this;
            }

            public LambdaSwitch<TKey> Default(Action action)
            {
                _default = action;
                return this;
            }

            public void Switch(TKey x) => (_matches.TryGetValue(x, out var a) ? a : _default)();
        }

        public class TypeSwitch
        {
            private Action<object> _default;
            private readonly Dictionary<Type, Action<object>> _matches = new();

            public TypeSwitch Case<T>(Action<T> action)
            {
                _matches.Add(typeof(T), (x) => action((T)x));
                return this;
            }


            public TypeSwitch Default(Action<object> action)
            {
                _default = action;
                return this;
            }

            public void Switch(object x)
            {
                var type = x?.GetType() ?? typeof(object);
                if (_matches.TryGetValue(type, out var action))
                {
                    action?.Invoke(x);
                }
                else
                {
                    _default?.Invoke(x);
                }
            } 
        }
    }
}