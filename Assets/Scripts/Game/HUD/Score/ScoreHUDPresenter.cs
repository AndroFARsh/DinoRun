using System;

namespace Game
{
    public class ScoreHUDPresenter
    {
        private event Action<int> _updateScore;
        public event Action<int> OnUpdateScore
        {
            remove => _updateScore -= value;
            add
            {
                value?.Invoke(_lastScore);
                _updateScore += value;
            }
        }

        private int _lastScore;

        ScoreHUDPresenter(ScoreBoard scoreBoard)
        {
            scoreBoard.OnScoreChanged += UpdateScore;
        }

        private void UpdateScore(int oldScore, int newScore)
        {
            _lastScore = newScore;
            _updateScore?.Invoke(_lastScore);
        }

    }
}