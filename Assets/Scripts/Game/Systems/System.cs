using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using Leopotam.EcsLite;

namespace Game
{
    public class System : ISystem, IDisposable
    {
        private readonly EcsWorld _world;
        private readonly CashedFactory _factory;

        private static IEcsSystems _system;
        
        public System(EcsWorld world, IFactory factory)
        {
            _world = world;
            _factory = new CashedFactory(factory);
        }
        
        public void Init()
        {
            _system = Builder()
                    .Register<TileBuildSystem>()
                    .Register<TileFlipSystem>()
                    .Register<TileTeardownSystem>()
                    
                    .Register<ParallaxBuildSystem>()
                    .Register<ParallaxStartMoveSystem>()
                    .Register<ParallaxMoveSystem>()
                    .Register<ParallaxTeardownSystem>()

                    .Register<GravitySystem>()

                    .Register<CharacterSpawnSystem>()
                    .Register<CharacterStartMoveSystem>()
                    .Register<CharacterGroundCheckSystem>()
                    .Register<CharacterLongJumpSystem>()
                    .Register<CharacterGroundJumpSystem>()
                    .Register<CharacterCrouchSystem>()
                    .Register<CharacterRunSystem>()
                    .Register<CharacterObstacleCheckSystem>()
                    .Register<CharacterAnimateSystem>()
                    .Register<CharacterTeardownSystem>()

                    .Register<ObstacleSpawnSystem>()
                    .Register<ObstacleSpawnCooldownSystem>()
                    .Register<ObstacleMoveSystem>()
                    .Register<ObstacleTeardownSystem>()

                    // move camera to start the game
                    .Register<CameraStartFollowSystem>()
                    .Register<CameraFollowSystem>()
                    .Register<CameraTeardownFollowSystem>()

                    .Register<VelocityApplySystem>()
                    .Register<ScoreCounterSystem>()
                    .Register<GameStartSystem>()
                    .Register<GameOverSystem>()
                    .Build();
            
            _system.Init();
        }

        public void Run() => _system.Run();
        
        public void Destroy() => _system.Destroy();


        public void Dispose() => _factory.Dispose();

        private SystemsBuilder Builder() => new (_world, _factory);

        private readonly struct SystemsBuilder
        {
            private readonly IFactory _factory;
            private readonly IEcsSystems _systems;

            public SystemsBuilder(EcsWorld world, IFactory factory)
            {
                _factory = factory;
                _systems = new EcsSystems(world);
            }

            public SystemsBuilder Register<T>() where T : IEcsSystem
            {
                _systems.Add(_factory.Create<T>());
                return this;
            }

            public IEcsSystems Build() => _systems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                ;
        }
        
        private class CashedFactory : IFactory, IDisposable
        {
            private readonly Dictionary<string, object> _cashedSystems = new ();
            private readonly IFactory _factory;

            public CashedFactory(IFactory factory)
            {
                _factory = factory;
            }

            public object Create(Type type, params object[] args)
            {
                var key = CreateKey(type, args);
                if (!_cashedSystems.ContainsKey(key))
                {
                    _cashedSystems.Add(key, _factory.Create(type, args));
                }

                return _cashedSystems[key];
            }

            public void Dispose() => _cashedSystems.Clear();
            
            private static string CreateKey(Type type, object[] args)
            {
                var builder = new StringBuilder(type.FullName);
                if (args is not { Length: > 0 }) return builder.ToString();
                
                foreach (var arg in args)
                {
                    builder.Append("|").Append(arg);
                }
                return builder.ToString();
            }
        }
    }
}