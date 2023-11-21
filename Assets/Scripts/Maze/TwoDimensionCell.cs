using TMPro;
using UnityEngine;

namespace Maze
{
    /// <summary>
    /// Display a 2D sprite that represent a cell
    /// </summary>
    public class TwoDimensionCell : MonoBehaviour
    {
        public ECellType Type { get { return m_InternalType; } }

        [SerializeField] private TMP_Text m_ValueText;
        [SerializeField] private TMP_Text m_TypeText;
        [SerializeField] private SpriteRenderer m_Sprite;
        [SerializeField] private GameObject m_SelectedSprite;

        private int m_InternalValue;
        private ECellType m_InternalType;

        private void Awake()
        {
            ToggleSelectedObject(false);
        }

        /// <summary>
        /// Initialize cell with value and position.
        /// Call it onse.
        /// </summary>
        /// <param name="_Value">Cell value</param>
        /// <param name="_Position">Cell position</param>
        /// <param name="_InternalType">Cell type</param>
        public void Initialize(int _Value, Vector2Int _Position, ECellType _InternalType)
        {
            transform.position = new Vector2(_Position.x, _Position.y);

            UpdateDisplay(_Value, _InternalType);
        }

        /// <summary>
        ///  Update GameObject name, sprite and text.
        /// </summary>
        /// <param name="_Value">Cell value</param>
        /// <param name="_InternalType">Cell type</param>
        public void UpdateDisplay(int _Value, ECellType _InternalType)
        {
            m_InternalValue = _Value;
            m_InternalType = _InternalType;

            UpdateName();
            UpdateValue();
            UpdateType();

            ToggleSelectedObject(false);
        }

        /// <summary>
        /// Show/Hide debug informations.
        /// </summary>
        /// <param name="isShow"></param>
        public void ToggleInfo(bool isShow)
        {
            if (isShow)
                ShowInfo();
            else
                HideInfo();
        }

        /// <summary>
        /// Convert to Cell Model.
        /// </summary>
        /// <returns></returns>
        public CellModel ToCellModel()
            => new CellModel(m_InternalValue, new Vector2Int((int)transform.position.x, (int)transform.position.y), m_InternalType);

        public void Selected()
            => ToggleSelectedObject(true);

        public void Deselected()
            => ToggleSelectedObject(false);

        private void UpdateName()
        {
            string prefix = m_InternalValue == -2 ? "Border" : m_InternalValue == -1 ? "Wall" : "Cell";
            name = prefix + $" [{transform.position.x},{transform.position.y}]";
        }

        private void UpdateValue()
        {
            m_Sprite.color = CellColorGenerator.GetColor(m_InternalValue);
            m_ValueText.text = m_InternalValue.ToString();
        }

        private void UpdateType()
            => m_TypeText.text = m_InternalType.ToString();

        private void ShowInfo()
        {
            UpdateValue();
            UpdateType();
        }

        private void HideInfo()
        {
            m_Sprite.color = CellColorGenerator.GetColorByType(m_InternalType);
            m_ValueText.text = string.Empty;
            m_TypeText.text = string.Empty;
        }

        private void ToggleSelectedObject(bool isSelected)
            => m_SelectedSprite.SetActive(isSelected);
    }
}