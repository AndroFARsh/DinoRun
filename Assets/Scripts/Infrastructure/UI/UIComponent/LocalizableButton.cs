using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

namespace UI
{
    public class LocalizableButton : Button
    {
        public LocalizedString localizedText
        {
            get;
            set;
        }

        public LocalizableButton() : this(string.Empty, string.Empty) { }
 
        public LocalizableButton(string table, string tableEntry)
            : this(CreateLocalizedString(table, tableEntry))
        {
        }
        
        public LocalizableButton(LocalizedString text)
        {
            localizedText = text;
        }

        protected override void ExecuteDefaultAction(EventBase evt)
        {
            base.ExecuteDefaultAction(evt);

            switch (evt)
            {
                case AttachToPanelEvent:
                    localizedText.StringChanged += HandleStringChanged;
                    break;
                case DetachFromPanelEvent:
                    localizedText.StringChanged -= HandleStringChanged;
                    break;
            }
        }

        private void HandleStringChanged(string val)
        {
            text = val;
        }

        private static LocalizedString CreateLocalizedString(string table, string tableEntry) =>
            new()
            {
                TableReference = string.IsNullOrEmpty(table) ? LocalizationSettings.StringDatabase.DefaultTable : table,
                TableEntryReference = tableEntry
            };
            

        public new class UxmlFactory: UxmlFactory<LocalizableButton, UxmlTraits> {}
        
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _table = new() { name = "locale-table" };
            private readonly UxmlStringAttributeDescription _tableEntry = new() { name = "locale-key" };
            private readonly UxmlStringAttributeDescription _text = new() { name = "text" };
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var button = ve as LocalizableButton;
                button.localizedText = CreateLocalizedString( _table.GetValueFromBag(bag, cc), _tableEntry.GetValueFromBag(bag, cc));
                button.text = _text.GetValueFromBag(bag, cc);
            }
        }
    }
}