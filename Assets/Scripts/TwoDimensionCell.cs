using TMPro;
using UnityEngine;

namespace Maze
{
    /// <summary>
    /// Display a 2D sprite that represent a cell
    /// </summary>
    public class TwoDimensionCell : MonoBehaviour
    {
        private SpriteRenderer m_Sprite;
        private TMP_Text m_Display;

        private void Awake()
        {
            m_Sprite = GetComponentInChildren<SpriteRenderer>();
            m_Display = GetComponentInChildren<TMP_Text>();
        }

        /// <summary>
        /// Initialize cell with value and position.
        /// Call it onse.
        /// </summary>
        /// <param name="_Value">Cell value</param>
        /// <param name="_Position">Cell position</param>
        public void Initialize(int _Value, Vector2Int _Position)
        {
            transform.position = new Vector2(_Position.x, _Position.y);

            UpdateDisplay(_Value);
        }

        /// <summary>
        /// Update GameObject name, sprite and text.
        /// </summary>
        /// <param name="_Value">Cell value</param>
        public void UpdateDisplay(int _Value)
        {
            UpdateName(_Value);
            UpdateValue(_Value);
        }

        private void UpdateName(int _Value)
        {
            string prefix = _Value == -2 ? "Border" : _Value == -1 ? "Wall" : "Cell";
            name = prefix + $" [{transform.position.x},{transform.position.y}]";
        }

        private void UpdateValue(int _Value)
        {
            m_Sprite.color = CellColorGenerator.GetColor(_Value);
            m_Display.text = _Value.ToString();
        }
    }
}