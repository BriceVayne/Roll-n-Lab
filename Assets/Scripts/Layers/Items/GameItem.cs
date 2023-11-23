using Maze;
using UnityEngine;

namespace Layers
{
    public class GameItem : LayerItem
    {
        [SerializeField] private SpriteRenderer m_Sprite;

        public override void InitializeItem(CellModel cell)
        {
        }

        public override void UpdateItem(CellModel cell)
        {
        }
    }
}