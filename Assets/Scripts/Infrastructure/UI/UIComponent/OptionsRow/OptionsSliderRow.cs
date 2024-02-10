using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UI
{
    public class OptionsSliderRow : OptionsRow
    {
        private const string ROW_CONTENT_LABLE_CLASS = "row_content_slider";
        
        private readonly Slider _contentSlider;

        public event Action<float, float> onValueChanged;
        
        public float value
        {
            get => _contentSlider.value;
            set => _contentSlider.value = value;
        }

        public OptionsSliderRow()
        {
            _contentSlider = new Slider();
            _contentSlider.AddToClassList(ROW_CONTENT_LABLE_CLASS);
            _contentSlider.RegisterValueChangedCallback(ValueChanged);
            
            content.Add(_contentSlider);
            
            RegisterCallback<FocusEvent>(OnFocus);
            RegisterCallback<BlurEvent>(OnBlur);
            
        }

        private void ValueChanged(ChangeEvent<float> v)
        {
            onValueChanged?.Invoke(v.previousValue, v.newValue);
        }

        private void OnFocus(FocusEvent e)
        {
            _contentSlider.AddToClassList($"{ROW_CONTENT_LABLE_CLASS}_focus");
        }
        
        private void OnBlur(BlurEvent e)
        {
            _contentSlider.RemoveFromClassList($"{ROW_CONTENT_LABLE_CLASS}_focus");
        }
        
        public new class UxmlFactory: UxmlFactory<OptionsSliderRow, UxmlTraits> {}
        
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _table = new() { name = "locale-table" };
            private readonly UxmlStringAttributeDescription _titleKey = new() { name = "locale-title-key" };
            
            private readonly UxmlStringAttributeDescription _title = new() { name = "title", defaultValue = "Option Label"};
            private readonly UxmlFloatAttributeDescription _value = new() { name = "value", defaultValue = 0};
            private readonly UxmlFloatAttributeDescription _lowValue = new() { name = "lowValue", defaultValue = 0};
            private readonly UxmlFloatAttributeDescription _highValue = new() { name = "highValue", defaultValue = 1};
            
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }
        
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var row = ve as OptionsSliderRow;
                
                row.title = _title.GetValueFromBag(bag, cc);
                row.value = _value.GetValueFromBag(bag, cc);
                row._contentSlider.lowValue = _lowValue.GetValueFromBag(bag, cc);
                row._contentSlider.highValue = _highValue.GetValueFromBag(bag, cc);
                
                row.table = _table.GetValueFromBag(bag, cc);
                row.tableEntry = _titleKey.GetValueFromBag(bag, cc);
            }
        }
    }
}