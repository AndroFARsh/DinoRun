using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;

namespace Game
{
    public class HUDFactory : IHUDFactory
    {
        private readonly IFactory _factory;
        private readonly IAssetProvider _assetProvider;
        private readonly IReadOnlyList<Type> _hud;

        HUDFactory(IFactory factory, IAssetProvider assetProvider, IReadOnlyList<Type> hud)
        {
            _factory = factory;
            _assetProvider = assetProvider;
            _hud = hud;
        }
        
        public async UniTask<HUD[]> Create() => await UniTask.WhenAll(_hud.Select(async t => await Create(t)));

        private async UniTask<HUD> Create(Type type)
        {
            var hud = (HUD)_factory.Create(type);
            await hud.Initialize(_assetProvider);
            return hud;
        }
    }
}