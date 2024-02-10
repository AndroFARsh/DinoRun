namespace Game
{
    public class JumpHUDPresenter
    {
        private readonly JumpScreenInput _screenInput;

        JumpHUDPresenter(JumpScreenInput screenInput)
        {
            _screenInput = screenInput;
        }

        public void PerformJumpStart() => _screenInput.Perform();

        public void PerformJumpEnd() => _screenInput.Release();
    }
}