using Infrastructure;
using Infrastructure.Utils;
using Leopotam.EcsLite;
using Services;
using SettingsMenu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private CrouchScreenInput _crouchScreenInputPrefab;
        [SerializeField] private JumpScreenInput _jumpScreenInputPrefab;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterParallax(builder);
            
            RegisterObstacleSpawnPoint(builder);
            
            RegisterAchieveService(builder);
            
            RegisterScoreBoard(builder);
            
            RegisterObstaclePool(builder);
            
            RegisterFactory(builder);

            RegisterWindowService(builder);

            RegisterSceneStateMachine(builder);

            RegisterHUD(builder);
            
            RegisterCamera(builder);

            RegisterGround(builder);

            RegisterSky(builder);

            RegisterCharacter(builder);
            
            RegisterPauseModal(builder);

            RegisterGameOverModal(builder);

            RegisterSettingsMenu(builder);

            RegisterTickableService(builder);
            
            RegisterEcsSystemsLifetime(builder);
        }

        private static void RegisterParallax(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ParallaxMono>().AsImplementedInterfaces();
        }

        private static void RegisterObstacleSpawnPoint(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ObstacleSpawnPointMono>().AsImplementedInterfaces();
        }

        private static void RegisterAchieveService(IContainerBuilder builder)
        {
            builder.Register<AchieveService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private static void RegisterScoreBoard(IContainerBuilder builder)
        {
            builder.Register<ScoreBoard>(Lifetime.Singleton);
        }

        private static void RegisterObstaclePool(IContainerBuilder builder)
        {
            builder.Register<ObstaclePool>(Lifetime.Singleton);
        }

        private static void RegisterCharacter(IContainerBuilder builder)
        {
            builder.Register<CharacterPool>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<CharacterSpawnPointMono>().AsImplementedInterfaces();
        }
        
        private static void RegisterCamera(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterComponentInHierarchy<CameraOffsetMono>().AsImplementedInterfaces();
        }
        
        private static void RegisterGround(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GroundFlipTileMono>().AsImplementedInterfaces();
        }

        private static void RegisterSky(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<SkyFlipTileMono>().AsImplementedInterfaces();
        }
        
        private static void RegisterTickableService(IContainerBuilder builder)
        {
            builder.Register<TickableService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private static void RegisterEcsSystemsLifetime(IContainerBuilder builder) {
            builder.RegisterInstance(new EcsWorld());
            builder.Register<System>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterHUD(IContainerBuilder builder)
        {
            builder.Register<HUDService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            var hud = builder.Register<HUDFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithReadOnlyListTypeParameter<HUD>();
            if (ProjectData.IsMobilePlatform)
            {
                hud.Register<JumpHUD>();
                hud.Register<CrouchHUD>();
            }
            hud.Register<ScoreHUD>();
            hud.Register<SettingsHUD>();

            builder.RegisterComponentInNewPrefab(_crouchScreenInputPrefab, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_jumpScreenInputPrefab, Lifetime.Singleton);

            builder.Register<JumpHUDPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<CrouchHUDPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<JoystickHUDPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<ScoreHUDPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<SettingsHUDPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private static void RegisterSceneStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        
        private static void RegisterFactory(IContainerBuilder builder)
        {
            builder.Register<Factory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        
        private static void RegisterWindowService(IContainerBuilder builder)
        {
            builder.Register<WindowService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        
        private static void RegisterPauseModal(IContainerBuilder builder)
        {
            builder.Register<PauseModalPresenter>(Lifetime.Singleton);
        }
        
        private static void RegisterGameOverModal(IContainerBuilder builder)
        {
            builder.Register<GameOverPresenter>(Lifetime.Singleton);
        }
        
        private static void RegisterSettingsMenu(IContainerBuilder builder)
        {
            builder.Register<SettingsMenuPresenter>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TabsFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithReadOnlyListTypeParameter<Tab>()
                .Register<GameSettingsTab>()
                .Register<AudioSettingsTab>();
            
            builder.Register<GameSettingsTabPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<AudioSettingsTabPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}