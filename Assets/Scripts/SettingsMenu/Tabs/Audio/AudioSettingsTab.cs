using System;
using Infrastructure;
using Services;
using UI;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace SettingsMenu
{
    public class AudioSettingsTab : Tab
    {
        public override LocalizedString Title => _title.localizedText;
        
        private readonly AudioSettingsTabPresenter _presenter;

        private OptionsSliderRow _musicRow;
        private OptionsSliderRow _fxRow;
        private LocalizableLabel _title;

        public AudioSettingsTab(AudioSettingsTabPresenter presenter) 
            : base(AssetKeys.AudioSettingsMenuUI)
        {
            _presenter = presenter;
        }
        
        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _musicRow = visualElement.Q<OptionsSliderRow>("music_value");
            _fxRow = visualElement.Q<OptionsSliderRow>("fx_value");
            _title = visualElement.Q<LocalizableLabel>("title");
            
            return visualElement;
        }

        protected override void OnAttached()
        {
            _musicRow.Next.clicked += UpMusicValue;
            _musicRow.Prev.clicked += DownMusicValue;
            _musicRow.onValueChanged += _presenter.RequestMusicValueChange;
            _presenter.OnMusicVolumeChanged += OnMusicValueChanged;
            
            
            _fxRow.Next.clicked += UpFxValue;
            _fxRow.Prev.clicked += DownFxValue;
            _fxRow.onValueChanged += _presenter.RequestFXValueChange;
            _presenter.OnEffectVolumeChanged += OnFxValueChanged;
        }

        protected override void OnDetached()
        {
            _presenter.OnMusicVolumeChanged -= OnMusicValueChanged;
            _musicRow.onValueChanged -= _presenter.RequestMusicValueChange;
            _musicRow.Next.clicked -= UpMusicValue;
            _musicRow.Prev.clicked -= DownMusicValue;
            
            _presenter.OnEffectVolumeChanged -= OnFxValueChanged;
            _fxRow.onValueChanged -= _presenter.RequestMusicValueChange;
            _fxRow.Next.clicked -= UpFxValue;
            _fxRow.Prev.clicked -= DownFxValue;
        }

        private void OnMusicValueChanged(float newValue)
        {
            if (Math.Abs(_musicRow.value - newValue) > 0.01f) _musicRow.value = newValue;
        }
        
        private void UpMusicValue() => _musicRow.value += 0.1f;
        
        private void DownMusicValue() => _musicRow.value -= 0.1f;

        
        private void OnFxValueChanged(float newValue)
        {
            if (Math.Abs(_fxRow.value - newValue) > 0.01f) _fxRow.value = newValue;
        }
        
        private void UpFxValue() => _fxRow.value += 0.1f;

        private void DownFxValue() => _fxRow.value -= 0.1f;
    }
}