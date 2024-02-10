using UnityEngine;

namespace Infrastructure
{
    public static class ProjectData
    {
        public static string Version => Application.version;
        public static bool IsMobilePlatform => Application.isMobilePlatform;
        public static RuntimePlatform Platform => Application.platform;
    }
}