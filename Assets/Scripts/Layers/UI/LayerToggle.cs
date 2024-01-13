using Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Layers
{
    internal sealed class LayerToggle : MonoBehaviour
    {
        [SerializeField] private Toggle m_Toggle;
        [SerializeField] private TMP_Text m_Label;
        private ELayerType m_InternalLayer;

        public void Bind(ELayerType _ELayer, bool _IsEnable)
        {
            m_InternalLayer = _ELayer;
            m_Label.text = _ELayer.ToString();
            m_Toggle.onValueChanged.AddListener(OnToggle);
            m_Toggle.isOn = _IsEnable;
        }

        public void Unbind()
        {
            m_InternalLayer = ELayerType.NONE;
            m_Label.text = string.Empty;
            m_Toggle.onValueChanged.RemoveAllListeners();
        }

        private void OnToggle(bool _IsOn)
            => LayerService.Instance.OnToggleLayer.Invoke(m_InternalLayer, _IsOn);
    }
}
