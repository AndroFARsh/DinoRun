using Infrastructure;
using Services;
using UnityEngine.UIElements;

namespace Loader
{
    public class BundleLoaderWindow : Window
    {
        private readonly BundleLoaderPresenter _presenter;
        
        private ProgressBar _progressBar;

        public BundleLoaderWindow(BundleLoaderPresenter presenter) : base(AssetKeys.LoaderUI)
        {
            _presenter = presenter;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _progressBar = visualElement.Q<ProgressBar>("progress");
            return visualElement;
        }

        protected override void OnAttached()
        {
            _presenter.OnProgress += UpdateProgress;
        }

        protected override void OnDetached()
        {
            _presenter.OnProgress -= UpdateProgress;
        }
        
        private void UpdateProgress(float p) => _progressBar.value = p;
    }
}