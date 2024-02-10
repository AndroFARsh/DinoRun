using Infrastructure;
using Services;
using UnityEngine.UIElements;

namespace Game
{
    public class GameOverModal : Modal
    {
        private readonly GameOverPresenter _presenter;
        
        private Button _restart;
        private Button _mainMenu;

        public override bool IsIgnoreCloseOutside => true;

        public GameOverModal(GameOverPresenter presenter) 
            : base(AssetKeys.GameOverModalUI)
        {
            _presenter = presenter;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _restart = visualElement.Q<Button>("restart");
            _mainMenu = visualElement.Q<Button>("main_menu");
            
            return base.OnInitialize(visualElement);
        }

        protected override void OnAttached()
        {
            _restart.clickable.clicked += _presenter.Restart;
            _mainMenu.clickable.clicked += _presenter.GameHub;
        }

        protected override void OnDetached()
        {
            _restart.clickable.clicked -= _presenter.Restart;
            _mainMenu.clickable.clicked -= _presenter.GameHub;
        }
    }
}