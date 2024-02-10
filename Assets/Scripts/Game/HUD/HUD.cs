using Infrastructure;

namespace Game
{
    public abstract class HUD : View
    {
        public abstract HUDAlign Align { get; }

        protected HUD(string contentRef) : base(contentRef)
        {
        }
    }
}