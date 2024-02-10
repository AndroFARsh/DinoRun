using Infrastructure;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace SettingsMenu
{
    public abstract class Tab : View
    {
        public abstract LocalizedString Title { get; }

        protected Tab(string contentRef) : base(contentRef)
        {
        }
    }
}