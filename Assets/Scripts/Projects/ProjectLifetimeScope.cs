using Infrastructure;
using Services;
using Services.Ads;
using Services.StaticData;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Projects
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        [SerializeField] private StaticData _staticData;
        [SerializeField] private CurtainMono _curtainMono;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterAds(builder);
                
            RegisterTextScale(builder);
            
            RegisterInput(builder);
            
            RegisterLogger(builder);

            RegisterMusicService(builder);
            
            RegisterAssetsProvider(builder);
            
            RegisterStateMachine(builder);

            RegisterLocalizationService(builder);
            
            RegisterSettingsService(builder);
            
            RegisterNavigationServices(builder);

            RegisterCurtain(builder, _curtainMono);
            
            RegisterStaticData(builder, _staticData);
            
            RegisterBaseFactories(builder);
        }

        private void RegisterAds(IContainerBuilder builder)
        {
            builder.Register<AdvertisementService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private static void RegisterTextScale(IContainerBuilder builder)
        {
            builder.Register<TextScaleService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private static void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private static void RegisterBaseFactories(IContainerBuilder builder)
        {
            builder.Register<Factory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private static void RegisterLogger(IContainerBuilder builder)
        {
            builder.Register<UnityLoggerService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        
        private static void RegisterMusicService(IContainerBuilder builder)
        {
            builder.Register<EffectService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MusicService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        
        private static void RegisterCurtain(IContainerBuilder builder, CurtainMono curtainMono)
        {
            builder.RegisterComponentInNewPrefab(curtainMono, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces();
        }
        
        private static void RegisterStaticData(IContainerBuilder builder, StaticData staticData)
        {
            builder.RegisterInstance(staticData);
        }

        
        private static void RegisterNavigationServices(IContainerBuilder builder)
        {
            builder.Register(IAppExitCommand.RegisterType(), Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<SceneManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<Navigation>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private static void RegisterSettingsService(IContainerBuilder builder)
        {
            builder.Register<DefaultSettingsService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<SettingsService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private static void RegisterLocalizationService(IContainerBuilder builder)
        {
            builder.Register<LocalizationService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        
        private static void RegisterAssetsProvider(IContainerBuilder builder)
        {
            builder.Register<AssetProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UpdateBundleService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private static void RegisterStateMachine(IContainerBuilder builder)
        {
            builder.Register<ProjectStateMachine>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}