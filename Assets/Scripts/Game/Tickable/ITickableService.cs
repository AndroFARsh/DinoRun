using System;

namespace Game
{
    public interface ITickableService {
        void Register(Action callback);
    
        void Unregister(Action callback);
    }
}