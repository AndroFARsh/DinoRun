using System;

namespace SettingsMenu
{
    public interface ISettingsMenuPresenter
    {
        event Action<float> OnTextSizeFactorChanged;
            
        void Back();

        void Reset();
    }
}