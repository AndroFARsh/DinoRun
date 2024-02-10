using System;
using Services;
using UI;
using UnityEngine.Localization;
using UnityEngine.UIElements;
using Locale = Services.Locale;

namespace SettingsMenu
{
    public class GameSettingsTab : Tab
    {
        public override LocalizedString Title => _title.localizedText;
        
        private readonly GameSettingsTabPresenter _presenter;

        private OptionsSliderRow _textSizeRow;
        private OptionsLabelRow _locationRow;
        private LocalizableLabel _title;
        
        public GameSettingsTab(GameSettingsTabPresenter presenter)
            : base(AssetKeys.GameSettingsMenuUI)
        {
            _presenter = presenter;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _locationRow = visualElement.Q<OptionsLabelRow>("text_language");
            _textSizeRow = visualElement.Q<OptionsSliderRow>("text_size");
            _title = visualElement.Q<LocalizableLabel>("title");
            
            return visualElement;
        }

        protected override void OnAttached()
        {
            _presenter.OnLocaleChanged += UpdateLocale;
            _locationRow.Next.clicked += _presenter.NextLocale;
            _locationRow.Prev.clicked += _presenter.PrevLocale;
            
            _textSizeRow.Next.clicked += UpTextSizeValue;
            _textSizeRow.Prev.clicked += DownTextSizeValue;
            _textSizeRow.onValueChanged += _presenter.RequestTextSizeValueChange;
            _presenter.OnTextSizeFactorChanged += OnTextSizeFactorChanged;
        }

        protected override void OnDetached()
        {
            _presenter.OnLocaleChanged -= UpdateLocale;
            _locationRow.Next.clicked -= _presenter.NextLocale;
            _locationRow.Prev.clicked -= _presenter.PrevLocale;
            
            _textSizeRow.Next.clicked -= UpTextSizeValue;
            _textSizeRow.Prev.clicked -= DownTextSizeValue;
            _textSizeRow.onValueChanged -= _presenter.RequestTextSizeValueChange;
            _presenter.OnTextSizeFactorChanged -= OnTextSizeFactorChanged;
        }

        private void UpdateLocale(Locale locale)
        {
            var text = locale.ToString();
            if (_locationRow.value != text) _locationRow.value = text;
        }

        private float _initialSize;
        private float _initialScale;
        
        private void OnTextSizeFactorChanged(float newValue)
        {
            if (Math.Abs(_textSizeRow.value - newValue) > 0.1f)
            {
                _textSizeRow.value = newValue;
            }
        }
        
        private void UpTextSizeValue() => _textSizeRow.value += 0.1f;
        
        private void DownTextSizeValue() => _textSizeRow.value -= 0.1f;
    }
}