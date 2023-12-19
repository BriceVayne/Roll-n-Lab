using Maze;

namespace Layers
{
    public class TypeItem : TextItem
    {
        protected override void SetText(CellModel cell)
           => m_Text.text = $"{cell.Type.ToString().ToLowerInvariant()}";
    }
}