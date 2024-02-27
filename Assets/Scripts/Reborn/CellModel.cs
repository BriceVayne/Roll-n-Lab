using UnityEngine;

namespace Reborn.Maze
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
    internal enum ECellType
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
        /// <summary>
        /// Internal cell value
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Internal cell position
        /// </summary>
        public Vector2Int Position { get; private set; }

        /// <summary>
        /// Internal cell type
        /// </summary>
        public ECellType Type { get; private set; }

        /// <summary>
        /// Internal cell offset
        /// </summary>
        public float ZOffset { get; private set; }

        /// <summary>
        /// Create cell data with a value and position
        /// </summary>
        /// <param name="Value">Internal cell value</param>
        /// <param name="Position">Internal cell position</param>
        /// <param name="ZOffset">Internal cell offset</param>
        public CellModel(int _Value, Vector2Int _Position, ECellType _Type, float _ZOffset = 0f)
        {
            Value = _Value;
            Position = _Position;
            Type = _Type;
            ZOffset = _ZOffset;
        }

        /// <summary>
        /// Copy cell data
        /// </summary>
        /// <param name="Model">Original cell to copy</param>
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

        /// <summary>
        /// Convert position and offset into Verctor3
        /// </summary>
        /// <returns></returns>
        public Vector3 ToVector3()
            => new Vector3(Position.x, Position.y, ZOffset);

        public override string ToString()
            => $"Cell[{Position.x},{Position.y}] = {Value}";
    }
}
