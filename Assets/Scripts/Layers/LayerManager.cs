using Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Layers
{
    [Serializable]
    public enum ELayer
    {
        GAME,
        COLOR,
        POSITION,
        VALUE,
        TYPE
    }

    [Serializable]
    public struct SLayer
    {
        public string LayerName;
        public ELayer Layer;
        public GameObject Prefab;
        public bool Enable;
    }

    public class LayerManager : Singleton<LayerManager>
    {
        [SerializeField] List<SLayer> LayerToItem = new List<SLayer>();

        private void Start()
        {
            foreach (var sLayer in LayerToItem)
            {
                if (!sLayer.Enable)
                    continue;

                var panel = Instantiate(sLayer.Prefab, transform);
            }
        }
    }

}