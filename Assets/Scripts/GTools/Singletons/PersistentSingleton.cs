using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GTools.Singletons
{
    public abstract class PersistentSingleton<T> : MonoBehaviour where T : PersistentSingleton<T>
    {
        public static T Instance;

        virtual protected void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Instance = this as T;
        }
    }
}