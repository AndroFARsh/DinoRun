using System.Linq;
using UnityEngine;

namespace CodeBase.Utils
{
    public static class ComponentUtils
    {
        public static bool TryGetComponentInChildren<T>(this GameObject go, out T value)
        {
            value = go ? go.GetComponentInChildren<T>() : default;
            return value != null;
        }

        public static T TryGetOrCreateComponent<T>(this GameObject go)
            where T : Component =>
            go.TryGetComponent<T>(out var result)
                ? result
                : go.AddComponent<T>();

        public static T TryGetOrCreateComponent<T>(this Component component)
            where T : Component => TryGetOrCreateComponent<T>(component.gameObject);

        public static T GetComponentInChildrenByName<T>(this GameObject go, string withName) where T : Component =>
            go.GetComponentsInChildren<T>().First(c => c.name == withName);

        public static T GetComponentInChildrenByName<T>(this Component component, string withName) where T : Component =>
            component.GetComponentsInChildren<T>().First(c => c.name == withName);

    }
}