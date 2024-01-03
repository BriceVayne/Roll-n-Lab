using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Layers
{
    public class LayerTimeline : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_MinLabel;
        [SerializeField] private TMP_Text m_MaxLabel;
        [SerializeField] private TMP_Text m_CurrentLabel;
        [SerializeField] private Slider m_Slider;

        private void Awake()
        {
            m_Slider.onValueChanged.AddListener((value) => m_CurrentLabel.text = value.ToString());
        }

        public void Bind(int _Min, int _Max, int _Current)
        {
            m_MinLabel.text = _Min.ToString();
            m_MaxLabel.text = _Max.ToString();
            m_CurrentLabel.text = _Current.ToString();

            m_Slider.minValue = _Min;
            m_Slider.maxValue = _Max;
            m_Slider.value = _Current;
            m_Slider.onValueChanged.AddListener(LayerManager.Instance.OnSliderChanged.Invoke);
        }

        public void Unbind()
        {
            m_MinLabel.text = string.Empty;
            m_MaxLabel.text = string.Empty;
            m_CurrentLabel.text = string.Empty;

            m_Slider.minValue = 0;
            m_Slider.maxValue = 1;
            m_Slider.value = 0;
            m_Slider.onValueChanged.RemoveListener(LayerManager.Instance.OnSliderChanged.Invoke);
        }
    }
}