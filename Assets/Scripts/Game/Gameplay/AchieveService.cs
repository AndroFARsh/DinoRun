using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services;
using Channel = Services.Channel;

namespace Game
{
    public class AchieveService : IAsyncInitializer, IDisposable
    {
        private readonly ScoreBoard _scoreBoard;
        private readonly IEffectService _effectService;

        AchieveService(ScoreBoard scoreBoard, IEffectService effectService)
        {
            _scoreBoard = scoreBoard;
            _effectService = effectService;
        }

        public UniTask Initialize()
        {
            _scoreBoard.OnScoreChanged += OnScoreChanged;
            return default;
        }
        
        public void Dispose()
        {
            _scoreBoard.OnScoreChanged -= OnScoreChanged;
        }
        
        private void OnScoreChanged(int oldScore, int newScore)
        {
            if (oldScore < newScore && newScore % 100 == 0)
            {
                PerformAchieveEffect();
            }
        }

        private void PerformAchieveEffect() => _effectService.PlayEffect(Effect.Achieve, Channel.Secondary);
    }
}