using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

namespace UI
{
    public abstract class OptionsRow : VisualElement
    {
        private const string ROW_TITLE_CLASS = "row_title";
        private const string ROW_CONTAINER_CLASS = "row_container";
        private const string ROW_PREV_BUTTON_CLASS = "row_prev_button";
        private const string ROW_CONTENT_CONTAINER_CLASS = "row_content_container";
        private const string ROW_NEXT_BUTTON_CLASS = "row_next_button";

        private readonly Label _titleLabel;
        private readonly VisualElement _rowContainer;

        private readonly Button _prevButton;
        private readonly Button _nextButton;

        private readonly VisualElement _contentContainer;
        private readonly LocalizedString _localisedString = new();

        private string _table;

        public string table
        {
            get => _table;
            set
            {
                _localisedString.TableReference = string.IsNullOrEmpty(value)
                    ? LocalizationSettings.StringDatabase.DefaultTable
                    : value;
                _table = value;
            }
        }

        private string _tableEntry;

        public string tableEntry
        {
            get => _tableEntry;
            set
            {
                _localisedString.TableEntryReference = value;
                _tableEntry = value;
            }
        }

        protected VisualElement content => _contentContainer;

        public string title
        {
            get => _titleLabel.text;
            set => _titleLabel.text = value;
        }

        public Clickable Next => _nextButton.clickable;

        public Clickable Prev => _prevButton.clickable;

        public OptionsRow()
        {
            focusable = true;

            style.flexGrow = new StyleFloat(1f);
            style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);

            _titleLabel = new Label
            {
                style =
                {
                    width = Length.Percent(50),
                    marginLeft = new Length(5f),
                    marginTop = new Length(0),
                    marginRight = new Length(0),
                    marginBottom = new Length(0),
                    paddingLeft = new Length(0),
                    paddingTop = new Length(0),
                    paddingRight = new Length(0),
                    paddingBottom = new Length(0),
                }
            };
            _titleLabel.AddToClassList(ROW_TITLE_CLASS);

            _rowContainer = new VisualElement
            {
                style =
                {
                    //"flex-grow: 1; flex-direction: row;
                    flexGrow = new StyleFloat(1f),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    width = Length.Percent(50),
                    marginRight = new Length(0),
                    marginLeft = new Length(5f),
                }
            };
            _rowContainer.AddToClassList(ROW_CONTAINER_CLASS);

            _prevButton = new Button
            {
                style =
                {
                    marginLeft = new Length(0),
                    marginTop = new Length(0),
                    marginRight = new Length(0),
                    marginBottom = new Length(0),
                    paddingLeft = new Length(0),
                    paddingTop = new Length(0),
                    paddingRight = new Length(0),
                    paddingBottom = new Length(0),
                    borderLeftWidth = new StyleFloat(0f),
                    borderTopWidth = new StyleFloat(0f),
                    borderRightWidth = new StyleFloat(0f),
                    borderBottomWidth = new StyleFloat(0f),
                    backgroundColor = new StyleColor(new Color(0f, 0f, 0f, 0f))
                }
            };
            _prevButton.AddToClassList(ROW_PREV_BUTTON_CLASS);
            _prevButton.focusable = false;

            _nextButton = new Button
            {
                style =
                {
                    marginLeft = new Length(0),
                    marginTop = new Length(0),
                    marginRight = new Length(0),
                    marginBottom = new Length(0),
                    paddingLeft = new Length(0),
                    paddingTop = new Length(0),
                    paddingRight = new Length(0),
                    paddingBottom = new Length(0),
                    borderLeftWidth = new StyleFloat(0f),
                    borderTopWidth = new StyleFloat(0f),
                    borderRightWidth = new StyleFloat(0f),
                    borderBottomWidth = new StyleFloat(0f),
                    backgroundColor = new StyleColor(new Color(0f, 0f, 0f, 0f))
                }
            };
            _nextButton.AddToClassList(ROW_NEXT_BUTTON_CLASS);
            _nextButton.focusable = false;

            _contentContainer = new VisualElement { style = { flexGrow = new StyleFloat(1f) } };
            _contentContainer.AddToClassList(ROW_CONTENT_CONTAINER_CLASS);

            _rowContainer.Add(_prevButton);
            _rowContainer.Add(_contentContainer);
            _rowContainer.Add(_nextButton);

            Add(_titleLabel);
            Add(_rowContainer);

            RegisterCallback<FocusEvent>(OnFocus);
            RegisterCallback<BlurEvent>(OnBlur);
        }

        private void OnFocus(FocusEvent e)
        {
            _titleLabel.AddToClassList($"{ROW_TITLE_CLASS}_focus");
            _nextButton.AddToClassList($"{ROW_NEXT_BUTTON_CLASS}_focus");
            _prevButton.AddToClassList($"{ROW_PREV_BUTTON_CLASS}_focus");
        }

        private void OnBlur(BlurEvent e)
        {
            _titleLabel.RemoveFromClassList($"{ROW_TITLE_CLASS}_focus");
            _nextButton.RemoveFromClassList($"{ROW_NEXT_BUTTON_CLASS}_focus");
            _prevButton.RemoveFromClassList($"{ROW_PREV_BUTTON_CLASS}_focus");
        }

        protected override void ExecuteDefaultAction(EventBase evt)
        {
            base.ExecuteDefaultAction(evt);

            switch (evt)
            {
                case AttachToPanelEvent:
                    _localisedString.StringChanged += HandleTitleChanged;
                    break;
                case DetachFromPanelEvent:
                    _localisedString.StringChanged -= HandleTitleChanged;
                    break;
            }
        }

        private void HandleTitleChanged(string val)
        {
            title = val;
        }
    }
}