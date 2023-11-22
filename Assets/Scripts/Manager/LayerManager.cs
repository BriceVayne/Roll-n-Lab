using Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Layers
{
    [Serializable]
    public enum ELayerType
    {
        GAME,
        COLOR,
        POSITION,
        VALUE,
        TYPE,
        NONE
    }

    [Serializable]
    public struct SLayer
    {
        public string Name;
        public ELayerType Type;
        public GameObject Prefab;
    }

    public class LayerManager : Singleton<LayerManager>
    {
        public delegate void OpenLayerDelegate(bool _IsOpen);
        public delegate void CreateLayerDelegate(ELayerType _ELayer);
        public delegate void ToggleLayerDelegate(ELayerType _ELayer, bool _IsOn);

        public static OpenLayerDelegate OnOpenLayer;
        public static CreateLayerDelegate OnCreateLayer;
        public static ToggleLayerDelegate OnToggleLayer;

        [SerializeField] private List<SLayer> m_Layers = new List<SLayer>();
        private Dictionary<ELayerType, GameObject> m_InternalPanelLayers;

        private void Awake()
        {
            OnOpenLayer = null;
            OnCreateLayer = null;
            OnToggleLayer = null;
        }

        private void Start()
        {
            foreach (var layer in m_Layers)
            {
                if (!m_InternalPanelLayers.ContainsKey(layer.Type))
                    m_InternalPanelLayers.Add(layer.Type, null);

                var panel = Instantiate(layer.Prefab, transform);


                m_InternalPanelLayers[layer.Type] = panel;

                OnCreateLayer.Invoke(layer.Type);
            }
        }
    }

}