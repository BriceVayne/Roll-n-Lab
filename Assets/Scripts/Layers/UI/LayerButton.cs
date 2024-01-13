using Service;
using UnityEngine;
using UnityEngine.UI;

namespace Layers
{
    internal sealed class LayerButton : MonoBehaviour
    {
        [SerializeField] private Button m_Button;
        [SerializeField] private GameObject m_Panel;
        private bool m_IntervalState;

        private void Start()
        {
            m_Button.onClick.AddListener(OnClick);
            m_IntervalState = false;

            LayerService.Instance.OnOpenLayer += m_Panel.SetActive;
        }

        private void OnClick()
        {
            m_IntervalState = !m_IntervalState;
            LayerService.Instance.OnOpenLayer.Invoke(m_IntervalState);
        }
    }
}
