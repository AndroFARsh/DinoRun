using System;
using UnityEngine;

namespace Game
{
    public class ScoreBoard
    {
        public event Action<int, int> OnScoreChanged;
        
        public int PrevScore
        {
            get;
            private set;
        }

        private float _elapsedTime;
        private int _currentScore;
        public int CurrentScore
        {
            get => _currentScore;
            private set
            {
                if (_currentScore != value) {
                    OnScoreChanged?.Invoke(_currentScore, value);
                    _currentScore = value;
                }
            }
        }

        public void Tick()
        {
            _elapsedTime += Time.deltaTime * 10;
            CurrentScore = (int)_elapsedTime;
        }

        public void Reset()
        {
            PrevScore = CurrentScore;
            CurrentScore = 0;
            _elapsedTime = 0;
        }
    }
}