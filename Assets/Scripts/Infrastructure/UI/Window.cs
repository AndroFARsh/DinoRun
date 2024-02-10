using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace Infrastructure
{
    public abstract class Window : View
    {
        protected Window(string contentRef) : base(contentRef) { }

        protected override UniTask<VisualElement> OnAsyncInitialize(VisualElement visualElement)
        {
            style.flexGrow = new StyleFloat(1);
            visualElement.style.flexGrow = new StyleFloat(1);
            return base.OnAsyncInitialize(visualElement);
        }
    }
}