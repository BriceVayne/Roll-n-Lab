using UnityEngine;

namespace Maze
{
    public struct CellModelJob
    {
        public int Value { get; set; }
        public Vector2Int Position { get; private set; }

        public CellModelJob(int value, Vector2Int position)
        {
            Value = value;
            Position = position;
        }

        public CellModelJob(CellModelJob _Cell)
        {
            Value = _Cell.Value;
            Position = _Cell.Position;
        }
    }

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
        /// Internal cell position
        /// </summary>
        public Vector2Int Position { get; private set; }

        /// <summary>
        /// Create cell data with a value and position
        /// </summary>
        /// <param name="value">Internal cell value</param>
        /// <param name="position">Internal cell position</param>
        public CellModel(int value, Vector2Int position)
        {
            Value = value;
            Position = position;
        }
    }
}
