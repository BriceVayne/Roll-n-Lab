using Maze;
using UnityEngine;

namespace Layers
{
    public class ColorItem : LayerItem
    {
        [SerializeField] private SpriteRenderer m_Sprite;

        public override void InitializeItem(CellModel cell)
        {
            m_Sprite.color = CellColorGenerator.GetColor(cell.Value);
        }

        public override void UpdateItem(CellModel cell)
        {
            m_Sprite.color = CellColorGenerator.GetColor(cell.Value);
        }
    }
}
