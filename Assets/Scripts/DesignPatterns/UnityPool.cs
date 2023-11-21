using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Patterns
{
    [Serializable]
    public struct SPool
    {
        public string PoolName;
        public MonoBehaviour Prefab;
    }

    public class UnityPool : MonoBehaviour
    {
        [SerializeField] private List<SPool> Pools = new List<SPool>();




        //public Dictionary<string,ObjectPool<MonoBehaviour>> Pools { get; private set; }

        //public UnityPool(bool _CollectionCheck, int _DefaultCapacity = 1, int _MaxSize = 100)
        //{
        //    Items = new ObjectPool<T>(CreateItem, GetItem, ReleaseItem, DestroyItem, _CollectionCheck, _DefaultCapacity, _MaxSize);
        //}

        private MonoBehaviour CreateItem()
        {
            return null;
        }

        private void GetItem(MonoBehaviour item)
        {

        }

        private void ReleaseItem(MonoBehaviour item)
        {

        }

        private void DestroyItem(MonoBehaviour item)
        {
        }
    }
}
