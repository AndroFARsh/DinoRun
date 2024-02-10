namespace Game
{
    public class CrouchHUDPresenter
    {
        private readonly CrouchScreenInput _screenInput;

        CrouchHUDPresenter(CrouchScreenInput screenInput)
        {
            _screenInput = screenInput;
        }

        public void PerformCrouchStart() => _screenInput.Perform();
        
        public void PerformCrouchEnd() => _screenInput.Release();
    }
}