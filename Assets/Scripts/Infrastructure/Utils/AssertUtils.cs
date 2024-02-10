using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public static class AssertUtils
    {
        public static void AssertIfAny<T>(this List<T> list, T value, string assert)
        {
#if UNITY_EDITOR
            foreach (var v in list)
            {
                if (v.Equals(value))
                {
                    Debug.Log(assert);
                    return;
                }
            }
#endif
        }
        
        public static void AssertIfAny<T>(this List<T> list, Func<T, bool> predicate, string assert)
        {
#if UNITY_EDITOR
            foreach (var v in list)
            {
                if (predicate.Invoke(v))
                {
                    Debug.Log(assert);
                    return;
                }
            }
#endif
        }
    }
}