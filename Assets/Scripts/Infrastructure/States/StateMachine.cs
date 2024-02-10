using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Projects;

namespace Infrastructure
{
    public class StateMachine: IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        private readonly HashSet<Type> _supportedStates = new ();
        private readonly IStateMachine _parent;
        private readonly IFactory _factory;
        private IState _currentState;

        public bool HasState => _currentState != null;

        protected StateMachine(IStateMachine parent, IFactory factory)
        {
            _parent = parent;
            _factory = factory;
        }
        
        public bool IsStateSupported<TState>() where TState : IState =>
            _supportedStates.Contains(typeof(TState)) || (_parent?.IsStateSupported<TState>() ?? false);

        protected void RegisterState<TState>() where TState : IState =>
            _supportedStates.Add(typeof(TState));

        public virtual async UniTask Enter<TState>() where TState : class, ISimpleState
        {
            var newState = Resolve<TState>();
            if (newState != null)
            {
                if(_currentState != null) await _currentState.Exit();
      
                _currentState = newState;
                await newState.Enter();
            }
            else if (_parent?.IsStateSupported<TState>() ?? false)
            {
                if(_currentState != null) await _currentState.Exit();
      
                _currentState = null;
                await _parent.Enter<TState>();
            }
        }

        public virtual async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var newState = Resolve<IPayloadState<TPayload>>();
            if (newState != null)
            {
                if(_currentState != null) await _currentState.Exit();
      
                _currentState = newState;
                await newState.Enter(payload);
            }
            else if (_parent?.IsStateSupported<TState>() ?? false)
            {
                if(_currentState != null) await _currentState.Exit();
      
                _currentState = null;
                await _parent.Enter<TState, TPayload>(payload);
            }
        }
        
        private TState Resolve<TState>() where TState : IState
        {
            var stateType = typeof(TState);
            if (!_supportedStates.Contains(stateType)) return default;
            
            if (!_states.ContainsKey(typeof(TState)))
            {
                _states.Add(typeof(TState), _factory.Create<TState>());
            }

            return (TState)_states[typeof(TState)];
        }
    }
}