using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInterfaces
{
    public class LayerPanel : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private Toggle m_ColorToggle;
        [SerializeField] private Toggle m_PositionToggle;
        [SerializeField] private Toggle m_ValueToggle;
        [SerializeField] private Toggle m_TypeToggle;

        Vector3 m_DragOffset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.GetPositionAndRotation(out var position, out var rotation);
            m_DragOffset = position + Input.mousePosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform rectTransform = (RectTransform)transform;
            rectTransform.SetPositionAndRotation(m_DragOffset+Input.mousePosition, Quaternion.identity);
        }
    }
}