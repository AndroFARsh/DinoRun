using System;
using Infrastructure;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Game
{
    public class PauseModal : Modal
    {
        private readonly PauseModalPresenter _presenter;
        private readonly InputService _inputService;

        private Button _resume;
        private Button _restart;
        private Button _options;
        private Button _mainMenu;

        public PauseModal(PauseModalPresenter presenter, InputService inputService) 
            : base(AssetKeys.PauseModalUI)
        {
            _presenter = presenter;
            _inputService = inputService;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _resume = visualElement.Q<Button>("resume");
            _restart = visualElement.Q<Button>("restart");
            _options = visualElement.Q<Button>("options");
            _mainMenu = visualElement.Q<Button>("main_menu");
            
            return base.OnInitialize(visualElement);
        }

        protected override void OnAttached()
        {
            _inputService.Decline += ExitModal;
            
            _resume.Focus();
            
            _resume.clickable.clicked += _presenter.Resume;
            _restart.clickable.clicked += _presenter.Restart;
            _options.clickable.clicked += _presenter.Options;
            _mainMenu.clickable.clicked += _presenter.MainMenu;
        }

        protected override void OnDetached()
        {
            _inputService.Decline -= ExitModal;
            
            _resume.clickable.clicked -= _presenter.Resume;
            _restart.clickable.clicked -= _presenter.Restart;
            _options.clickable.clicked -= _presenter.Options;
            _mainMenu.clickable.clicked -= _presenter.MainMenu;
        }

        private void ExitModal(InputAction.CallbackContext context)
        {
            Debug.Log("ESC");
            _presenter.Resume();   
        }
    }
}