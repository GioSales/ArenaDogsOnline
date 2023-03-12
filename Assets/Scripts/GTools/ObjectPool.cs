using System.Collections.Generic;
using UnityEngine;

namespace GTools
{
    public class ObjectPool<T> where T : Component
    {
        private List<T> pool;
        private T prefab;
        private Transform parentTransform;
        private int maxPoolSize;
        private bool canExpand;

        public ObjectPool(T prefab, int initialCapacity, int maxPoolSize, bool canExpand, Transform parentTransform = null)
        {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            this.maxPoolSize = maxPoolSize;
            this.canExpand = canExpand;
            pool = new List<T>(initialCapacity);

            for (int i = 0; i < initialCapacity; i++)
            {
                T obj = Object.Instantiate(prefab, parentTransform);
                obj.gameObject.SetActive(false);
                pool.Add(obj);
            }
        }

        public T GetObject()
        {
            foreach (T obj in pool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            if (canExpand && pool.Count < maxPoolSize)
            {
                T newObj = Object.Instantiate(prefab, parentTransform);
                newObj.gameObject.SetActive(true);
                pool.Add(newObj);
                return newObj;
            }

            return null;
        }

        public void ReleaseObject(T obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}

