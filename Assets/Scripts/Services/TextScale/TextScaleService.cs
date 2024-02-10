using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine.UIElements;

namespace Services
{
    public class TextScaleService : ITextScaleService, IDisposable
    {
        class FontData
        {
            public bool Initialized => InitialFontSizeData > 0;
            public float LastSizeFactor;
            public float InitialFontSizeData;
        }
        
        private readonly ISettingsService _settingsService;
        private readonly Dictionary<VisualElement, FontData> _elements = new ();

        TextScaleService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _settingsService.OnTextSizeFactorChanged += OnTextSizeFactorChanged;
        }

        public void Register(View view) => RegisterVisualElement(view);
        
        public void Unregister(View view) => UnregisterVisualElement(view);
        
        public void Dispose() => _settingsService.OnTextSizeFactorChanged -= OnTextSizeFactorChanged;

        private void OnGeometryChangedEvent(GeometryChangedEvent ev)
        {
            if (ev.target == ev.currentTarget &&  ev.target is VisualElement element && _elements.TryGetValue(element, out var data) && !data.Initialized)
            {
                element.UnregisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
                
                data.InitialFontSizeData = element.resolvedStyle.fontSize;
                data.LastSizeFactor = _settingsService.TextSizeFactor;
                
                element.style.fontSize = data.InitialFontSizeData * data.LastSizeFactor;
            }
        }

        private void OnTextSizeFactorChanged(float sizeFactor)
        {
            foreach (var (element, data) in _elements)
            {
                if (data.Initialized && Math.Abs(data.LastSizeFactor - sizeFactor) > 0.1f)
                {
                    element.style.fontSize = data.InitialFontSizeData * sizeFactor;
                }
            }
        }
        
        private void RegisterVisualElement(VisualElement el)
        {
            if (!_elements.ContainsKey(el))
            {
                _elements.Add(el, new FontData());
                el.RegisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
            }

            foreach (var child in el.Children())
            {
                RegisterVisualElement(child);
            }
        }
        
        private void UnregisterVisualElement(VisualElement el)
        {
            if (_elements.Remove(el))
            {
                el.UnregisterCallback<GeometryChangedEvent>(OnGeometryChangedEvent);
            }

            foreach (var child in el.Children())
            {
                UnregisterVisualElement(child);
            }
        }
    }
}