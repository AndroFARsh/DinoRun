using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace SettingsMenu
{
    public class TabsFactory : ITabsFactory
    {
        private readonly IFactory _factory;
        private readonly IReadOnlyList<Type> _tabs;
        private readonly IAssetProvider _assetProvider;

        public TabsFactory(IFactory factory, IAssetProvider assetProvider, IReadOnlyList<Type> tabs)
        {
            _assetProvider = assetProvider;
            _factory = factory;
            _tabs = tabs;
        }

        public async UniTask<Tab[]> Create() => await UniTask.WhenAll(_tabs.Select(async t => await Create(t)));

        private async UniTask<Tab> Create(Type type)
        {
            var tab = (Tab)_factory.Create(type);
            await tab.Initialize(_assetProvider);
            return tab;
        } 
    }
}