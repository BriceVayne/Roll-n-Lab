using TMPro;
using UnityEngine;

public class CellModel
{
    public int Value { get; set; }
    public Color Color { get; set; }
    public Vector2 Position { get; set; }

    public CellModel(int value)
    {
        Value = value;
    }

    public CellModel(int value, Color color)
    {
        Value = value;
        Color = color;
    }

    public CellModel(int value, Color color, Vector2 position)
    {
        Value = value;
        Color = color;
        Position = position;
    }
}

public class Cell : MonoBehaviour
{
    private SpriteRenderer m_Sprite;
    private TMP_Text m_Display;
    private CellModel model;

    private void Awake()
    {
        m_Sprite = GetComponentInChildren<SpriteRenderer>();
        m_Display = GetComponentInChildren<TMP_Text>();
    }

    public void InitializeData(CellModel cellModel)
    {
        model = cellModel;
        
        UpdateData();
        UpdateName();
    }

    public void UpdateData()
    {
        transform.position = model.Position;
        m_Display.text = model.Value.ToString();
        m_Sprite.color = model.Color;
    }

    public void UpdateName()
    {
        string prefix = model.Value == -2 ? "Border" : model.Value == -1 ? "Wall" : "Cell";
        name = prefix + $" [{model.Position.x},{model.Position.y}]";
    }

    public int Value { get { return model.Value; } set { model.Value = value; } }
    public Color Color { get { return model.Color; } set { model.Color = value; } }

}