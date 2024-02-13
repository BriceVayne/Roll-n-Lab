using Maze;

namespace Layers
{
    public class ColorItem : ImageItem
    {
        protected override void SetSprite(CellModel cell)
            => m_Sprite.color = ColorGenerator.GetColor(cell.Value);
    }
}
