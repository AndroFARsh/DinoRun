using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.Utils;
using Services;
using UnityEngine.UIElements;

namespace Game
{
    public enum HUDAlign
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public class HUDService : IHUDService
    {
        private readonly Dictionary<HUDAlign, VisualElement> _hudContainer = new ();
        private readonly ITextScaleService _textScaleService;
        private readonly IAssetProvider _assetProvider;
        private readonly IHUDFactory _hudFactory;

        private UIDocument _document;
        private VisualElement _root;
        private HUD[] _huds;
        
        HUDService(IAssetProvider assetProvider, IHUDFactory hudFactory, ITextScaleService textScaleService)
        {
            _textScaleService = textScaleService;
            _assetProvider = assetProvider;
            _hudFactory = hudFactory;
        }

        public async UniTask Initialize()
        {
            var go = await _assetProvider.CreateGameObject(AssetKeys.HUDContainer);
            
            _document = go.GetComponent<UIDocument>();
            _root = _document.rootVisualElement;
            _root.RegisterCallback<NavigationSubmitEvent>((evt) => evt.StopPropagation(), TrickleDown.TrickleDown);
            
            _hudContainer.Add(HUDAlign.TopLeft, _root.Q<VisualElement>("left_toolbar"));
            _hudContainer.Add(HUDAlign.TopRight, _root.Q<VisualElement>("right_toolbar"));
            _hudContainer.Add(HUDAlign.BottomLeft, _root.Q<VisualElement>("left_footer"));
            _hudContainer.Add(HUDAlign.BottomRight, _root.Q<VisualElement>("right_footer"));
            
            _huds = await _hudFactory.Create();
            _huds.ForEach(AddHUD);
        }

        private void AddHUD(HUD hud)
        {
            if (_hudContainer.TryGetValue(hud.Align, out var container))
            {
                _textScaleService.Register(hud);
                
                container.Add(hud);
            }
        }

        public void Dispose()
        {
            foreach (var hud in _huds)
            {
                _textScaleService.Unregister(hud);
                
                hud.RemoveFromHierarchy();
                hud.Dispose();
            }
        }
    }
}