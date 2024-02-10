using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Game
{
    public class TickableService : ITickableService, ITickable
    {
        private event Action _callbacks;

        public void Tick() => _callbacks?.Invoke();
        
        public void Register(Action callback) => _callbacks += callback;

        public void Unregister(Action callback) => _callbacks -= callback;
    }
}