using Layers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterfaces
{
    public class LayerPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Content;
        [SerializeField] private LayerToggle m_Prefab;
        List<LayerToggle> m_Items;

        private void Awake()
        {
            m_Items = new List<LayerToggle>();
        }

        private void Start()
        {
            LayerManager.OnCreateLayer += CreateLayer;
            gameObject.SetActive(false);
        }

        private void CreateLayer(ELayerType _ELayer)
        {
            var toggle = Instantiate(m_Prefab, m_Content);
            toggle.Bind(_ELayer);
            m_Items.Add(toggle);
        }
    }
}