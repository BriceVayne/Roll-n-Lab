using Maze;

namespace Layers
{
    public class ValueItem : TextItem
    {
        protected override void SetText(CellModel cell)
           => m_Text.text = $"{cell.Value}";
    }
}