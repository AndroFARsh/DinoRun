using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UI
{
    public class Panel : VisualElement
    {
        private int _activeChild;

        public int pageCount => childCount;

        public void NextPage() => SendDirectionEvent(NavigationMoveEvent.Direction.Next);

        public void PrevPage() => SendDirectionEvent(NavigationMoveEvent.Direction.Previous);

        public void ActivatePage(int page)
        {
            _activeChild = pageCount > 0 ? Math.Clamp(page, 0, pageCount - 1) : 0;

            var index = 0;
            foreach (var child in Children())
            {
                var visible = _activeChild == index++;
                child.style.visibility = visible ? Visibility.Visible : Visibility.Hidden;
                child.style.display =visible ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
        
        protected override void ExecuteDefaultAction(EventBase evt)
        {
            base.ExecuteDefaultAction(evt);

            if (evt is (AttachToPanelEvent or DetachFromPanelEvent))
            {
                ActivatePage(_activeChild);
            }

            if (evt is NavigationMoveEvent moveEvent)
            {
                var direction = moveEvent.direction switch
                {
                    NavigationMoveEvent.Direction.Next => 1,
                    NavigationMoveEvent.Direction.Previous => -1,
                    _ => 0
                };

                ActivatePage(_activeChild + direction);
            }
        }

        private void SendDirectionEvent(NavigationMoveEvent.Direction direction)
        {
            using var ev = NavigationMoveEvent.GetPooled(direction);
            ev.target = this;
            SendEvent(ev);
        }
        
        public new class UxmlFactory: UxmlFactory<Panel, UxmlTraits> {}

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlIntAttributeDescription _visibleChild = new() { name = "activeChild", defaultValue = 0};
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var panel = ve as Panel;

                panel._activeChild = _visibleChild.GetValueFromBag(bag, cc);
            }
        }
    }
}