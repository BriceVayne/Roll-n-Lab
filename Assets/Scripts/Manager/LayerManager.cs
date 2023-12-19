using Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    public bool IsReady { get; }
}

namespace Layers
{
    public class LayerManager : Singleton<LayerManager>, IManager
    {
        public static Action<bool> OnOpenLayer;
        public static Action<ELayerType> OnCreateLayer;
        public static Action<ELayerType, bool> OnToggleLayer;
        
        public bool IsReady { get; private set; }

        [SerializeField] private Transform layerContent;
        [SerializeField] private List<SLayer> m_Layers = new List<SLayer>();

        private const float Z_OFFSET = 0.01f;

        private Dictionary<ELayerType, Layer> m_InternalPanelLayers;

        private void Awake()
        {
            IsReady = false;

            OnOpenLayer = null;
            OnCreateLayer = null;
            OnToggleLayer = null;

            OnToggleLayer += ToggleALayer;

            m_InternalPanelLayers = new Dictionary<ELayerType, Layer>();

            Debug.Log("Layer Manager Awake");
        }

        private void Start()
        {
            InitializeLayers();

            IsReady = true;
        }

        private void ToggleALayer(ELayerType _ELayer, bool _IsOn)
        {
            if (m_InternalPanelLayers.TryGetValue(_ELayer, out var panel))
                panel.gameObject.SetActive(_IsOn);
        }

        private void InitializeLayers()
        {
            int i = 0;
            foreach (var layer in m_Layers)
            {
                if (!m_InternalPanelLayers.ContainsKey(layer.Type))
                    m_InternalPanelLayers.Add(layer.Type, null);

                var panel = Instantiate(layer.Prefab, layerContent);
                panel.transform.position = new Vector3(0f, 0f, Z_OFFSET + (Z_OFFSET * i));

                m_InternalPanelLayers[layer.Type] = panel;

                OnCreateLayer.Invoke(layer.Type);

                i++;
            }
        }
    }
}