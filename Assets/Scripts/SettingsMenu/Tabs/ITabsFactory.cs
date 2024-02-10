using Cysharp.Threading.Tasks;

namespace SettingsMenu
{
    public interface ITabsFactory
    {
        public UniTask<Tab[]> Create();
    }
}