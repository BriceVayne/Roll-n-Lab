using Maze;

public class PositionItem : TextItem
{
    protected override void SetText(CellModel cell)
        => m_Text.text = $"[{cell.Position.x},{cell.Position.y}]";
}
