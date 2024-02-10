using Leopotam.EcsLite;

namespace Infrastructure.Utils
{
    public static class EcsFilterUtils
    {
        public static bool IsEmpty(this EcsFilter filter) => (filter?.GetEntitiesCount() ?? 0) == 0;
        
        public static bool IsNotEmpty(this EcsFilter filter) => (filter?.GetEntitiesCount() ?? 0) > 0;
    }
}