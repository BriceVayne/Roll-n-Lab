using Maze;
using UnityEngine;

namespace Layers
{
    public class ImageItem : LayerItem
    {
        [SerializeField] protected SpriteRenderer m_Sprite;

        public override void InitializeItem(CellModel _Cell)
        {
            transform.position = _Cell.GetPosition();
            name = $"{_Cell.Type} [{_Cell.Position.x},{_Cell.Position.y}]";

            SetSprite(_Cell);
        }

        public override void UpdateItem(CellModel _Cell)
            => SetSprite(_Cell);

        protected virtual void SetSprite(CellModel _Cell) { }
    }
}