using Maze;

namespace Extension
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
}