using System;
using VContainer;

namespace Infrastructure
{
    public class Factory : IFactory
    {
        private readonly IObjectResolver _resolver;

        public Factory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        public object Create(Type type, params object[] args)
        {
            var registrationBuilder = new RegistrationBuilder(type, Lifetime.Transient);
            if (args is { Length: > 0 })
            {
                foreach (var arg in args)
                {
                    registrationBuilder.WithParameter(arg.GetType(), arg);
                }
            }

            var registration = registrationBuilder.Build();
            var instance = registration.SpawnInstance(_resolver);
            return instance;
        }
    }
}