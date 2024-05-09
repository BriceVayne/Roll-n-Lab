using UnityEngine;

namespace Maze
{
    internal static class Extensions
    {
        public static CellModel[,] Copy(this CellModel[,] from)
        {
            CellModel[,] to = new CellModel[from.GetLength(0), from.GetLength(1)];

            for (int x = 0; x < from.GetLength(0); x++)
                for (int y = 0; y < from.GetLength(1); y++)
                    to[x, y] = new CellModel(from[x, y]);

            return to;
        }
    }

    /// <summary>
    /// The cell type list
    /// </summary>
    public enum ECellType
    {
        BORDER,
        WALL,
        START,
        PATH,
        END,
        EMPTY
    }

    internal sealed class CellModel
    {
        public int Value { get; private set; }
        public Vector2Int Position { get; private set; }
        public ECellType Type { get; private set; }
        public float ZOffset { get; private set; }

        public CellModel(int _Value, Vector2Int _Position, ECellType _Type, float _ZOffset = 0f)
        {
            Value = _Value;
            Position = _Position;
            Type = _Type;
            ZOffset = _ZOffset;
        }

        public CellModel(CellModel _Model)
        {
            Value = _Model.Value;
            Position = _Model.Position;
            Type = _Model.Type;
            ZOffset = _Model.ZOffset;
        }

        public void SetValue(int _Value)
            => Value = _Value;

        public void SetType(ECellType _Type)
            => Type = _Type;

        public Vector3 ToVector3()
            => new Vector3(Position.x, Position.y, ZOffset);

        public override string ToString()
            => $"Cell[{Position.x},{Position.y}] = {Value}";
    }
}
