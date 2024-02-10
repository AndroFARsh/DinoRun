using Services;
using UnityEngine.UIElements;

namespace Game
{
    public class ScoreHUD : HUD
    {
        private readonly ScoreHUDPresenter _presenter;
        private TextElement _scoreValue;

        public override HUDAlign Align => HUDAlign.TopLeft;
        
        public ScoreHUD(ScoreHUDPresenter presenter) 
            : base(AssetKeys.ScoreHUDUI)
        {
            _presenter = presenter;
        }

        protected override VisualElement OnInitialize(VisualElement visualElement)
        {
            _scoreValue = visualElement.Q<TextElement>("score_value");
            return visualElement;
        }
        
        protected override void OnAttached()
        {
            _presenter.OnUpdateScore += OnUpdateScore;
        }

        protected override void OnDetached()
        {
            _presenter.OnUpdateScore -= OnUpdateScore;
        }

        private void OnUpdateScore(int score) => _scoreValue.text = score.ToString();
    }
}