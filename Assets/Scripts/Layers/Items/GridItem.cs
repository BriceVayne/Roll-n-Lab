using Maze;
using UnityEngine;

namespace Layers
{
    public class GridItem : ImageItem
    {
        [SerializeField] private Sprite m_WallSprite;
        [SerializeField] private Sprite m_PathSprite;
        protected override void SetSprite(CellModel cell)
            => m_Sprite.sprite = cell.Type == ECellType.WALL || cell.Type == ECellType.BORDER ? m_WallSprite : m_PathSprite;
    }
}