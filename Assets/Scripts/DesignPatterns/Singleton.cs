using System;
using UnityEngine;

namespace Patterns
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance => LazyInstance.Value;

        private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);

        private static T CreateSingleton()
        {
            var anyInstance = FindAnyObjectByType<T>();
            if (anyInstance == null)
            {
                var ownerObject = new GameObject($"{typeof(T).Name} [SINGLETON]");
                anyInstance = ownerObject.AddComponent<T>();
            }
            
            DontDestroyOnLoad(anyInstance);

            return anyInstance;
        }
    }
}
