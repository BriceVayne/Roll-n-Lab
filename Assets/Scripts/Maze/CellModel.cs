using UnityEngine;

namespace Maze
{
    /// <summary>
    /// Model data to maze cell
    /// </summary>
    public class CellModel
    {
        /// <summary>
        /// Internal cell value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Internal cell type
        /// </summary>
        public ECellType Type { get; set; }

        /// <summary>
        /// Internal cell position
        /// </summary>
        public Vector2Int Position { get; private set; }

        /// <summary>
        /// Create cell data with a value and position
        /// </summary>
        /// <param name="value">Internal cell value</param>
        /// <param name="position">Internal cell position</param>
        public CellModel(int _Value, Vector2Int _Position, ECellType _Type)
        {
            Value = _Value;
            Position = _Position;
            Type = _Type;
        }

        /// <summary>
        /// Create cell data from another cell
        /// </summary>
        /// <param name="model">Origin cell</param>
        public CellModel(CellModel model)
        {
            Value = model.Value;
            Position = model.Position;
            Type = model.Type;
        }

        public Vector3 GetPosition()
           => new Vector3(Position.x, Position.y, 0f);

        public override string ToString()
        {
            return $"Cell[{Position.x},{Position.y}] is {Type} with {Value} value";
        }
    }
}
