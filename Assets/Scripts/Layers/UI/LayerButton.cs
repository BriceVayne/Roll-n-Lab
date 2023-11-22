using UnityEngine;
using UnityEngine.UI;

namespace Layers
{
    public class LayerButton : MonoBehaviour
    {
        [SerializeField] private Button m_Button;
        [SerializeField] private GameObject m_Panel;
        private bool m_IntervalState;

        private void Start()
        {
            m_Button.onClick.AddListener(OnClick);
            m_IntervalState = false;

            LayerManager.OnOpenLayer += m_Panel.SetActive;
        }

        private void OnClick()
        {
            m_IntervalState = !m_IntervalState;
            LayerManager.OnOpenLayer.Invoke(m_IntervalState);
        }
    }
}
