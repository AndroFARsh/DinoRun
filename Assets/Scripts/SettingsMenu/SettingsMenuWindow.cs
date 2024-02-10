using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;
using UI;
using UnityEngine.UIElements;

namespace SettingsMenu
{
    public class SettingsMenuWindow : Window
    {
        private readonly ISettingsMenuPresenter _presenter;
        private readonly ITabsFactory _tabsFactory;
        
        private IEnumerable<Tab> _tabs;
        private TabContainer _tabContainer;
        private Button _backButton;
        private Button _resetButton;

        private StyleLength _backButtonInitialSize;
        private StyleLength _resetButtonInitialSize;

        public SettingsMenuWindow(ISettingsMenuPresenter presenter, ITabsFactory tabsFactory)
            : base(AssetKeys.SettingsMenuUI)
        {
            _presenter = presenter;
            _tabsFactory = tabsFactory;
        }

        protected override async UniTask<VisualElement> OnAsyncInitialize(VisualElement visualElement)
        {
            _tabs = await _tabsFactory.Create();
            
            _tabContainer = visualElement.Q<TabContainer>("tab_container");
            _backButton = visualElement.Q<Button>("back");
            _resetButton = visualElement.Q<Button>("reset");

            _backButtonInitialSize = _backButton.resolvedStyle.fontSize;
            _resetButtonInitialSize = _resetButton.style.fontSize;
            
            return await base.OnAsyncInitialize(visualElement);
        }

        protected override void OnAttached()
        {
            foreach (var tab in _tabs)
            {
                _tabContainer.AddTab(tab.Title, tab);
            }
            
            
            _backButton.clickable.clicked += _presenter.Back;
            _resetButton.clickable.clicked += _presenter.Reset;
            _presenter.OnTextSizeFactorChanged += UpdateTextSize;
        }

        protected override void OnDetached()
        {
            _tabContainer.ClearTabs();

            _backButton.clickable.clicked -= _presenter.Back;
            _resetButton.clickable.clicked -= _presenter.Reset;
            
            _presenter.OnTextSizeFactorChanged -= UpdateTextSize;
        }

        private void UpdateTextSize(float factor)
        {
            //_backButton.style.fontSize = _backButtonInitialSize.value.value * factor;
            //_resetButton.style.fontSize = _resetButtonInitialSize.value.value * factor;
        }
    }
}