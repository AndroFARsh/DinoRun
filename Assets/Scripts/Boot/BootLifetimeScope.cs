using Infrastructure;
using VContainer;
using VContainer.Unity;

namespace Boot
{
    public class BootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterFactory(builder);
            RegisterSceneStateMachine(builder);
        }

        private static void RegisterFactory(IContainerBuilder builder) =>
            builder.Register<Factory>(Lifetime.Singleton).AsImplementedInterfaces();

        private static void RegisterSceneStateMachine(IContainerBuilder builder) =>
            builder.Register<BootStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();
        
    }
}