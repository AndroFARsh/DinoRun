using System;

namespace Infrastructure
{
    public interface IFactory
    {
        object Create(Type type, params object[] args);
        T Create<T>(params object[] args) => (T)Create(typeof(T), args);
    }
}