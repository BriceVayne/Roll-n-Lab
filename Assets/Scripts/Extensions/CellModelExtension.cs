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

        public static bool IsNeighboor(this CellModel from, CellModel to)
        {
            if ((to.Position.x - 1 == from.Position.x || to.Position.x + 1 == from.Position.x) &&
                (to.Position.y - 1 == from.Position.y || to.Position.y + 1 == from.Position.y))
                return true;
            else
                return false;
        }

        public static bool IsNeighboor(this TwoDimensionCell from, TwoDimensionCell to)
        {
            if (from.transform.position.x + 1 == to.transform.position.x && from.transform.position.y == to.transform.position.y ||
                from.transform.position.x - 1 == to.transform.position.x && from.transform.position.y == to.transform.position.y ||
                from.transform.position.y + 1 == to.transform.position.y && from.transform.position.x == to.transform.position.x ||
                from.transform.position.y - 1 == to.transform.position.y && from.transform.position.x == to.transform.position.x)
                return true;
            else
                return false;
        }
    }
}