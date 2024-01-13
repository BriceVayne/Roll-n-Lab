using Layers;
using Service;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterfaces
{
    internal sealed class LayerPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform m_Content;
        [SerializeField] private LayerToggle m_TogglePrefab;
        [SerializeField] private LayerTimeline m_SliderPrefab;

        private List<LayerToggle> m_Items;
        private LayerTimeline m_Timeline;

        private void Awake()
        {
            m_Items = new List<LayerToggle>();
            m_Timeline = null;
        }

        private void Start()
        {
            LayerService.Instance.OnCreateLayer += CreateLayer;
            LayerService.Instance.OnProcessEnd += CreateTimeline;
            gameObject.SetActive(false);
        }

        private void CreateLayer(ELayerType _ELayer, bool _IsEnable)
        {
            var toggle = Instantiate(m_TogglePrefab, m_Content);
            toggle.Bind(_ELayer, _IsEnable);
            m_Items.Add(toggle);
        }

        private void CreateTimeline(int _Min, int _Max, int _Current)
        {
            var slider = Instantiate(m_SliderPrefab, m_Content);
            slider.Bind(_Min, _Max, _Current);
            m_Timeline = slider;
        }
    }
}