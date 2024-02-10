using Infrastructure;
using Infrastructure.Utils;
using MainMenu;
using Services;
using SettingsMenu;
using VContainer;
using VContainer.Unity;

namespace Hub
{
    public class HubLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterFactory(builder);
            
            RegisterSettingsMenu(builder);
            
            RegisterMainMenu(builder);

            RegisterWindowService(builder);
            
            RegisterSceneStateMachine(builder);
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
        
        private static void RegisterMainMenu(IContainerBuilder builder)
        {
            builder.Register<MainMenuPresenter>(Lifetime.Singleton);
        }

        private static void RegisterWindowService(IContainerBuilder builder)
        {
           builder.Register<WindowService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
   
        private static void RegisterSceneStateMachine(IContainerBuilder builder)
        {
            builder.Register<HubStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        
        private static void RegisterFactory(IContainerBuilder builder)
        {
            builder.Register<Factory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}