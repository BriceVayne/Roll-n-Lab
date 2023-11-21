using Layers;
using Maze;
using TMPro;
using UnityEngine;

public class TextItem : LayerItem
{
    [SerializeField] protected TMP_Text m_Text;

    public override void InitializeItem(CellModel cell)
        => SetText(cell);

    public override void UpdateItem(CellModel cell)
        => SetText(cell);

    protected virtual void SetText(CellModel cell) { }
}
