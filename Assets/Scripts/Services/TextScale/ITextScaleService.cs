using Infrastructure;

namespace Services
{
    public interface ITextScaleService
    {
        void Register(View view);

        void Unregister(View view);
    }
}