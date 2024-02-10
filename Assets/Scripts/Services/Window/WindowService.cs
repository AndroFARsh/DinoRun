using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.Utils;
using UnityEngine.UIElements;

namespace Services
{
    public class WindowService : IWindowService, IDisposable
    {
        private readonly IFactory _factory;
        private readonly IAssetProvider _assetProvider;
        private readonly ITextScaleService _textScaleService;

        private readonly Stack<View> _stack = new();
        private readonly Dictionary<Type, View> _views = new();

        private UIDocument _document;

        private VisualElement _root;
        private VisualElement _windowContainer;
        private VisualElement _modalBackground;
        private VisualElement _modalContainer;

        WindowService(IFactory factory, IAssetProvider assetProvider, ITextScaleService textScaleService)
        {
            _factory = factory;
            _assetProvider = assetProvider;
            _textScaleService = textScaleService;
        }

        public async UniTask Initialize()
        {
            var go = await _assetProvider.CreateGameObject(AssetKeys.WindowsContainer);
            _document = go.GetComponent<UIDocument>();
            _root = _document.rootVisualElement.Q("root");
            _root.pickingMode = PickingMode.Ignore;

            _windowContainer = _root.Q("window_container");
            _windowContainer.pickingMode = PickingMode.Ignore;

            _modalBackground = _root.Q<VisualElement>("modal_background");
            _modalContainer = _root.Q<VisualElement>("modal_container");

            ModalVisibility(false);
        }

        public async UniTask Push<T>() where T : View
        {
            var view = await GetOrCreateWindow<T>();
            if (view is Modal)
            {
                _modalContainer.Clear();
                _modalContainer.Add(view);
                ModalVisibility(true);
            }
            else
            {
                ModalVisibility(false);
                _windowContainer.Clear();
                _windowContainer.Add(view);
            }
            
            _stack.Push(view);
        }

        public async UniTask Replace<T>() where T : View
        {
            Pop();
            await Push<T>();
        }

        public void Pop()
        {
            if (!_stack.TryPop(out var view)) return;

            if (view is Modal)
            {
                PopModal();
            }
            else
            {
                PopWindow();
            }
        }

        private void PopWindow()
        {
            _windowContainer.Clear();
            if (!_stack.TryPeek(out var nextView)) return;

            if (nextView is Modal)
            {
                ModalVisibility(true);
                _modalContainer.Add(nextView);
            }
            else
            {
                _windowContainer.Add(nextView);
            }
        }

        private void PopModal()
        {
            _modalContainer.Clear();
            if (_stack.TryPeek(out var nextView))
            {
                if (nextView is Modal)
                {
                    _modalContainer.Add(nextView);
                }
                else
                {
                    ModalVisibility(false);
                    _windowContainer.Add(nextView);
                }
            }
            else
            {
                ModalVisibility(false);
            }
        }

        public void Dispose()
        {
            _stack.Clear();

            foreach (var view in _views.Values)
            {
                _textScaleService.Register(view);
                view.Dispose();
            }

            _views.Clear();
        }

        private async UniTask<T> GetOrCreateWindow<T>() where T : View
        {
            if (_views.TryGetValue(typeof(T), out var view))
            {
                return (T)view;
            }

            view = _factory.Create<T>();
            await view.Initialize(_assetProvider);

            _views.Add(typeof(T), view);
            _textScaleService.Register(view);
            return (T)view;
        }

        private void ModalVisibility(bool visible)
        {
            if (_modalBackground.visible == visible) return;

            _modalBackground.visible = visible;
            if (visible)
            {
                _modalBackground.pickingMode = PickingMode.Position;
                _modalBackground.RegisterCallback<ClickEvent>(OnOutsideClick);
                _modalContainer.RegisterCallback<ClickEvent>(OnInsideClick);
            }
            else
            {
                _modalBackground.pickingMode = PickingMode.Ignore;
                _modalBackground.UnregisterCallback<ClickEvent>(OnOutsideClick);
                _modalContainer.UnregisterCallback<ClickEvent>(OnInsideClick);
            }
        }

        private static void OnInsideClick(EventBase evt)
        {
            // stop event propagation down so we don't close modal if we click inside modal
            evt.StopImmediatePropagation();
        }

        private void OnOutsideClick(EventBase evt)
        {
            // close top most modal on click outside
            if (!_stack.TryPeek(out var view) || view is not Modal { IsIgnoreCloseOutside: false }) return;

            evt.StopImmediatePropagation();
            Pop();
        }
    }
}