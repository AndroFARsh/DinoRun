using System;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace UI
{
    public class TabContainer : VisualElement
    {
        public const string TAB_BAR_CLASS = "tab_bar";
        public const string TAB_CONTENT_CLASS = "tab_content";
        public const string TAB_BUTTON_DEFAULT_CLASS = "tab_button_default";
        public const string TAB_BUTTON_ACTIVE_CLASS = "tab_button_active";

        private int _activeTabIndex;
        private readonly List<VisualElement> _tabs = new ();
        private readonly List<VisualElement> _tabButtons = new ();
        
        private readonly VisualElement _tabBar;
        private readonly VisualElement _tabContent;

        private IStyle TabBarStyle => _tabBar.style;
        private IStyle TabContent => _tabContent.style;

        public TabContainer()
        {
            _tabBar = new VisualElement
            {
                style =
                {
                    flexGrow = new StyleFloat(0f),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row)
                }
            };
            _tabContent = new VisualElement
            {
                style = { flexGrow = new StyleFloat(1) }
            };
            
            _tabBar.AddToClassList(TAB_BAR_CLASS);
            _tabContent.AddToClassList(TAB_CONTENT_CLASS);
            
            Add(_tabBar);
            Add(_tabContent);
        }

        public void AddTab(LocalizedString title, VisualElement content)
        {
            var index = _tabs.Count;
            var button = new LocalizableButton(title);
            
            button.AddToClassList(TAB_BUTTON_DEFAULT_CLASS);
            button.clickable.clicked += () => { PerformTabSelection(index); };

            _tabs.Add(content);
            _tabBar.Add(button);
            _tabButtons.Add(button);

            if (_tabs.Count == 1)
            {
                SelectTab(0);
            }
        }
        
        public void RemoveTabAt(int index)
        {
            if (index < 0 || index >= _tabs.Count) return;
            
            _tabs.RemoveAt(index);
            _tabBar.RemoveAt(index);
            _tabButtons.RemoveAt(index);

            if (index >= _activeTabIndex)
            {
                SelectTab(_activeTabIndex - 1);
            }
        }

        public void ClearTabs()
        {
            while (_tabs.Count > 0)
            {
                RemoveTabAt(_tabs.Count - 1);
            }
        }

        protected override void ExecuteDefaultAction(EventBase evt)
        {
            base.ExecuteDefaultAction(evt);
            switch (evt)
            {
                case (AttachToPanelEvent or DetachFromPanelEvent):
                    PerformTabSelection(_activeTabIndex);
                    break;
                case NavigationMoveEvent moveEvent:
                    PerformTabSelection(_activeTabIndex + moveEvent.direction switch
                    {
                        NavigationMoveEvent.Direction.Next => 1,
                        NavigationMoveEvent.Direction.Previous => -1,
                        _ => 0
                    });
                    break;
            }
        }
        
        public void NextTab() => SendDirectionEvent(NavigationMoveEvent.Direction.Next);

        public void PrevTab() => SendDirectionEvent(NavigationMoveEvent.Direction.Previous);

        public void SelectTab(int nextTabIndex)
        {
            if (nextTabIndex >= 0 && nextTabIndex < _tabs.Capacity)
            {
                _activeTabIndex = nextTabIndex;
                var button = _tabButtons[_activeTabIndex];
                
                var ev = NavigationSubmitEvent.GetPooled();
                ev.target = button;

                button.SendEvent(ev);
            }
        }

        private void PerformTabSelection(int nextTabIndex)
        {
            _activeTabIndex = _tabs.Count > 0 ? Math.Clamp(nextTabIndex, 0, _tabs.Count-1) : 0;
            
            for (var i = 0; i < _tabButtons.Count; ++i)
            {
                var button = _tabButtons[i];
                if (i == _activeTabIndex)
                {
                    button.AddToClassList(TAB_BUTTON_ACTIVE_CLASS);
                    button.RemoveFromClassList(TAB_BUTTON_DEFAULT_CLASS);
                }
                else
                {
                    button.RemoveFromClassList(TAB_BUTTON_ACTIVE_CLASS);
                    button.AddToClassList(TAB_BUTTON_DEFAULT_CLASS);
                }
            }

            var nextTab = _activeTabIndex >=0 && _activeTabIndex < _tabs.Count ? _tabs[_activeTabIndex] : null;
            
            _tabContent.Clear();
            if (nextTab != null)
            {
                _tabContent.Add(nextTab);
                RequestFocus(nextTab);
            }
        }

        private bool RequestFocus(VisualElement element)
        {
            if (element.focusable && element.canGrabFocus)
            {
                element.Focus();
                return true;
            }

            foreach (var child in element.Children())
            {
                if (RequestFocus(child))
                {
                    return true;
                }
            }

            return false;
        }

        private void SendDirectionEvent(NavigationMoveEvent.Direction direction)
        {
            using var ev = NavigationMoveEvent.GetPooled(direction);
            ev.target = this;
            SendEvent(ev);
        }
        
        public new class UxmlFactory: UxmlFactory<TabContainer, UxmlTraits> {}

        public new class UxmlTraits : VisualElement.UxmlTraits {}
    }
}