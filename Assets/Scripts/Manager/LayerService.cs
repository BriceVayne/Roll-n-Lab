using Layers;
using Maze;
using Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    internal sealed class LayerService : Singleton<LayerService>
    {
        public Action<bool> OnOpenLayer;
        public Action<ELayerType, bool> OnCreateLayer;
        public Action<ELayerType, bool> OnToggleLayer;
        public Action<int,int,int> OnProcessEnd;
        public Action<float> OnSliderChanged;

        [SerializeField] private Transform layerContent;
        [SerializeField] private List<SLayer> m_Layers = new List<SLayer>();

        private const float Z_OFFSET = 0.01f;

        private Dictionary<ELayerType, Layer> m_InternalPanelLayers;

        private float m_CurrentTime;
        private float m_Time;
        private int m_Index;
        private List<CellModel[,]> m_SnapShots;
        private bool m_StopUpdate;

        private void Awake()
        {
            OnOpenLayer = null;
            OnCreateLayer = null;
            OnToggleLayer = null;
            OnSliderChanged = null;

            OnToggleLayer += ToggleALayer;
            OnSliderChanged += ForceGridToUpdate;

            m_InternalPanelLayers = new Dictionary<ELayerType, Layer>();

            m_CurrentTime = 0f;
            m_Time = 0.01f;
            m_Index = 0;
        }

        private void Start()
        {
            InitializeLayers();

            StartCoroutine(WaitForSnapShot());
        }

        private void FixedUpdate()
        {
            if (m_Index >= m_SnapShots.Count)
            {
                OnProcessEnd.Invoke(0, m_SnapShots.Count, m_Index);
                enabled = false;
            }

            m_CurrentTime += Time.deltaTime;
            if(m_CurrentTime >= m_Time)
            {
                m_Index++;
    
                UpdateGrid(m_SnapShots[m_Index]);

                m_CurrentTime = 0f;
            }
        }

        private void ToggleALayer(ELayerType _ELayer, bool _IsOn)
        {
            if (m_InternalPanelLayers.TryGetValue(_ELayer, out var panel))
                panel.gameObject.SetActive(_IsOn);
        }

        private void ForceGridToUpdate(float _Index)
            => UpdateGrid(m_SnapShots[(int)_Index]);

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

                OnCreateLayer.Invoke(layer.Type, layer.IsEnable);

                i++;
            }
        }

        private IEnumerator WaitForSnapShot()
        {
            yield return new WaitUntil(() => GridService.Instance.SnapShots == null);

            m_SnapShots = GridService.Instance.SnapShots;
            m_Index = 0;

            CreateGrid(m_SnapShots[0]);
        }

        private void CreateGrid(CellModel[,] _SnapShot)
        {
            for (int x = 0; x < _SnapShot.GetLength(0); x++)
                for (int y = 0; y < _SnapShot.GetLength(1); y++)
                    GridService.Instance.OnCellCreated.Invoke(_SnapShot[x, y]);
        }

        private void UpdateGrid(CellModel[,] _SnapShot)
        {
            for (int x = 0; x < _SnapShot.GetLength(0); x++)
                for (int y = 0; y < _SnapShot.GetLength(1); y++)
                    GridService.Instance.OnCellUpdated.Invoke(_SnapShot[x, y]);
        }
    }
}