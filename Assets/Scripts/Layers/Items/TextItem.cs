using Maze;
using TMPro;
using UnityEngine;

namespace Layers
{
    public class TextItem : LayerItem
    {
        [SerializeField] protected TMP_Text m_Text;

        public override void InitializeItem(CellModel _Cell)
        {
            transform.position = _Cell.GetPosition();
            name = $"{_Cell.Type} [{_Cell.Position.x},{_Cell.Position.y}]";

            SetText(_Cell);
        }

        public override void UpdateItem(CellModel _Cell)
            => SetText(_Cell);

        protected virtual void SetText(CellModel _Cell) { }
    }
}