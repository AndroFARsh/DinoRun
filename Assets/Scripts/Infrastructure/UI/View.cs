using System;
using Cysharp.Threading.Tasks;
using Services;
using UnityEngine.UIElements;

namespace Infrastructure
{
    public abstract class View : VisualElement, IDisposable
    {
        private readonly string _contentRef;
        
        protected View(string contentRef)
        {
            _contentRef = contentRef;
        }

        public async UniTask Initialize(IAssetProvider assetProvider)
        {
            var prefab = await assetProvider.Load<VisualTreeAsset>(_contentRef);
            var content = prefab.Instantiate();
            
            Add(await OnAsyncInitialize(content));
            
            RegisterCallback<AttachToPanelEvent>(OnAttached);
            RegisterCallback<DetachFromPanelEvent>(OnDetached);
        }

        public virtual void Dispose()
        {
            UnregisterCallback<AttachToPanelEvent>(OnAttached);
            UnregisterCallback<DetachFromPanelEvent>(OnDetached);
        }

        protected virtual UniTask<VisualElement> OnAsyncInitialize(VisualElement visualElement) => 
            UniTask.FromResult(OnInitialize(visualElement));

        protected virtual VisualElement OnInitialize(VisualElement visualElement) => visualElement;

        private void OnAttached(AttachToPanelEvent ev) => OnAttached();

        protected abstract void OnAttached();
        
        private void OnDetached(DetachFromPanelEvent ev) => OnDetached();
        
        protected abstract void OnDetached();
    }
}