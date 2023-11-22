using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Layers
{
    public class LayerToggle : MonoBehaviour
    {
        [SerializeField] private Toggle m_Toggle;
        [SerializeField] private TMP_Text m_Label;
        private ELayerType m_InternalLayer;

        public void Bind(ELayerType _ELayer)
        {
            m_InternalLayer = _ELayer;
            m_Label.text = _ELayer.ToString();
            m_Toggle.onValueChanged.AddListener(OnToggle);
        }

        public void Unbind()
        {
            m_InternalLayer = ELayerType.NONE;
            m_Label.text = string.Empty;
            m_Toggle.onValueChanged.RemoveAllListeners();
        }

        private void OnToggle(bool _IsOn)
            => LayerManager.OnToggleLayer.Invoke(m_InternalLayer, _IsOn);
    }
}
