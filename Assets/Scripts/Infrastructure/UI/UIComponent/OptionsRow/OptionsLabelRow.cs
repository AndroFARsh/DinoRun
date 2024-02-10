using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UI
{
    public class OptionsLabelRow : OptionsRow
    {
        private const string ROW_CONTENT_LABLE_CLASS = "row_content_label";
        
        private readonly Label _contentLabel;
        
        public string value
        {
            get => _contentLabel.text;
            set => _contentLabel.text = value;
        }

        public OptionsLabelRow()
        {
            _contentLabel = new Label();
            _contentLabel.AddToClassList(ROW_CONTENT_LABLE_CLASS);
            
            content.Add(_contentLabel);
            
            RegisterCallback<FocusEvent>(OnFocus);
            RegisterCallback<BlurEvent>(OnBlur);
        }

        private void OnFocus(FocusEvent e)
        {
            _contentLabel.AddToClassList($"{ROW_CONTENT_LABLE_CLASS}_focus");
        }
        
        private void OnBlur(BlurEvent e)
        {
            _contentLabel.RemoveFromClassList($"{ROW_CONTENT_LABLE_CLASS}_focus");
        }
        
        public new class UxmlFactory: UxmlFactory<OptionsLabelRow, UxmlTraits> {}
        
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _table = new() { name = "locale-table" };
            private readonly UxmlStringAttributeDescription _titleKey = new() { name = "locale-title-key" };
            
            private readonly UxmlStringAttributeDescription _title = new() { name = "title", defaultValue = "Option Label"};
            private readonly UxmlStringAttributeDescription _value = new() { name = "value", defaultValue = "Option Value"};
            
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }
        
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var row = ve as OptionsLabelRow;
                
                row.title = _title.GetValueFromBag(bag, cc);
                row.value = _value.GetValueFromBag(bag, cc);
                row.table = _table.GetValueFromBag(bag, cc);
                row.tableEntry = _titleKey.GetValueFromBag(bag, cc);
            }
        }
    }
}