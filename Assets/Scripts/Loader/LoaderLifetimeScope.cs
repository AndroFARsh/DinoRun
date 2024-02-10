using Infrastructure;
using Services;
using VContainer;
using VContainer.Unity;

namespace Loader
{
    public class LoaderLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterFactory(builder);
            
            RegisterBundleLoader(builder);
            
            RegisterWindowService(builder);
            
            RegisterSceneStateMachine(builder);
        }
        
        private static void RegisterBundleLoader(IContainerBuilder builder) =>
            builder.Register<BundleLoaderPresenter>(Lifetime.Singleton);
        
        private static void RegisterWindowService(IContainerBuilder builder) =>
           builder.Register<WindowService>(Lifetime.Singleton).AsImplementedInterfaces();
        
        private static void RegisterSceneStateMachine(IContainerBuilder builder) =>
            builder.Register<LoaderStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();
        
        private static void RegisterFactory(IContainerBuilder builder) =>
            builder.Register<Factory>(Lifetime.Singleton).AsImplementedInterfaces();
    }
}