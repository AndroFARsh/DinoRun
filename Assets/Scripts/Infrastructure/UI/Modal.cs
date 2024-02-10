namespace Infrastructure
{
    public abstract class Modal : View
    {
        public virtual bool IsIgnoreCloseOutside => true;

        protected Modal(string contentRef) : base(contentRef) { }
    }
}