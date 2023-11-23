using Patterns;
using System.Collections.Generic;
using UnityEngine;

namespace Layers
{
    public class LayerManager : Singleton<LayerManager>
    {
        public delegate void OpenLayerDelegate(bool _IsOpen);
        public delegate void CreateLayerDelegate(ELayerType _ELayer);
        public delegate void ToggleLayerDelegate(ELayerType _ELayer, bool _IsOn);

        public static OpenLayerDelegate OnOpenLayer;
        public static CreateLayerDelegate OnCreateLayer;
        public static ToggleLayerDelegate OnToggleLayer;

        [SerializeField] private List<SLayer> m_Layers = new List<SLayer>();
        private const float Z_OFFSET = 0.01f;
        private Dictionary<ELayerType, Layer> m_InternalPanelLayers;

        private void Awake()
        {
            OnOpenLayer = null;
            OnCreateLayer = null;
            OnToggleLayer = null;

            OnToggleLayer += ToggleALayer;

            m_InternalPanelLayers = new Dictionary<ELayerType, Layer>();
        }

        private void Start()
        {
            InitializeLayers();
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

                var panel = Instantiate(layer.Prefab, transform);
                panel.transform.position = new Vector3(0f, 0f, Z_OFFSET + (Z_OFFSET * i));

                m_InternalPanelLayers[layer.Type] = panel;

                OnCreateLayer.Invoke(layer.Type);

                i++;
            }
        }
    }
}