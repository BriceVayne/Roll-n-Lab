using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Maze
{
    public class GridGenerator : MonoBehaviour
    {
        public int Size { get { return m_MazeSize; } }

        [Header("Maze Parameters")]
        [SerializeField] private int m_MazeSize = 32;
        [SerializeField][Range(1, 10)] private int m_SaveIterationImage = 1;
        [Space]
        [Header("2D Maze")]
        [SerializeField] private bool shouldGenerateTwoDimensionMaze = true;
        [SerializeField] private Transform m_Grid2D;
        [SerializeField] private TwoDimensionCell m_Prefab;
        [Space]
        [Header("3D Maze")]
        [SerializeField] private bool shouldGenerateThreeDimensionMaze = true;
        [SerializeField] private Transform m_Grid3D;
        [SerializeField] private GameObject m_WallPrefab;
        [SerializeField] private GameObject m_FloorPrefab;

        private CellModel[,] m_Maze;
        private List<CellModel> m_Walls;
        private List<List<CellModel>> m_CellBlocks;
        private HashSet<CellModel> m_Path;
        private int m_Number;
        private Queue<CellModel[,]> m_Iterations;

        private StringBuilder debugMessage;

        private void Awake()
        {
            m_Maze = new CellModel[m_MazeSize, m_MazeSize];
            m_Walls = new List<CellModel>();
            m_CellBlocks = new List<List<CellModel>>();
            m_Path = new HashSet<CellModel>();
            m_Number = 0;
            m_Iterations = new Queue<CellModel[,]>();

            debugMessage = new StringBuilder("===== Debug Log =====\n");
        }

        private void Start()
        {
            GenerateGrid();
            DeterminePath();
            ResolvedMaze();

            //using (StreamWriter outputFile = new StreamWriter(Path.Combine(Application.dataPath, "RunAndDry.txt")))
            //{
            //    outputFile.WriteLine(debugMessage);
            //    outputFile.Close();
            //    Debug.Log("Run & Dry !");
            //}
        }

        private void GenerateGrid()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int x = 0; x < m_MazeSize; x++)
            {
                for (int y = 0; y < m_MazeSize; y++)
                {
                    /// Set Value from position
                    int value = 0;
                    if (x == 0 || y == 0 || x == m_MazeSize - 1 || y == m_MazeSize - 1)
                        value = -2;
                    else if (x % 2 == 0 || y % 2 == 0)
                        value = -1;
                    else
                        value = m_Number++;

                    /// Create cell and initialize it
                    m_Maze[x, y] = new CellModel(value, new Vector2Int(x, y));

                    /// Excluse internal wall
                    if (value == -1)
                        m_Walls.Add(m_Maze[x, y]);
                    else if (value >= 0)
                        m_CellBlocks.Add(new List<CellModel>() { m_Maze[x, y] });
                }
            }

            stopwatch.Stop();
            double seconds = stopwatch.ElapsedMilliseconds / 1000f;
            debugMessage.Append($"Generation time: {seconds} seconds");
            Debug.Log($"Generation time: {seconds} seconds");
        }

        private void DeterminePath()
        {
            m_Path.Add(m_CellBlocks[0][0]);
            m_Path.Add(m_CellBlocks[m_CellBlocks.Count - 1][0]);
        }

        private void ResolvedMaze()
        {
            bool warHorizontal = false;
            int iteration = 0;

            m_Iterations.Enqueue(m_Maze);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (IsNotResolved())
            {
                Stopwatch stopwatchIt = new Stopwatch();
                stopwatchIt.Start();

                CellModel c = m_Walls[Random.Range(0, m_Walls.Count)];
                CellModel c1, c2;

                if ((c.Position.x == 1 && c.Position.y != 1) || (c.Position.x == m_MazeSize - 2 && c.Position.y != m_MazeSize - 2))
                {
                    c1 = m_Maze[c.Position.x, c.Position.y - 1];
                    c2 = m_Maze[c.Position.x, c.Position.y + 1];
                }
                else if ((c.Position.y == 1 && c.Position.x != 1) || (c.Position.y == m_MazeSize - 2))
                {
                    c1 = m_Maze[c.Position.x - 1, c.Position.y];
                    c2 = m_Maze[c.Position.x + 1, c.Position.y];
                }
                else
                {
                    if (warHorizontal)
                    {
                        c1 = m_Maze[c.Position.x, c.Position.y - 1];
                        c2 = m_Maze[c.Position.x, c.Position.y + 1];
                    }
                    else
                    {
                        c1 = m_Maze[c.Position.x - 1, c.Position.y];
                        c2 = m_Maze[c.Position.x + 1, c.Position.y];
                    }

                    warHorizontal = !warHorizontal;
                }

                if (c1.Value != -1 && c2.Value != -1 && c1.Value != c2.Value)
                {
                    List<CellModel> blockFromC1 = m_CellBlocks.Find(i => i.Contains(c1));
                    List<CellModel> blockFromC2 = m_CellBlocks.Find(i => i.Contains(c2));

                    // Move the minus list into the greatest
                    if(blockFromC1.Count >= blockFromC2.Count)
                    {
                        if (!blockFromC1.Contains(c))
                            blockFromC1.Add(c);

                        foreach (var cell in blockFromC2)
                        {
                            if (!blockFromC1.Contains(cell))
                                blockFromC1.Add(cell);
                        }

                        foreach (var cell in blockFromC1)
                            cell.Value = c1.Value;

                        m_CellBlocks.Remove(blockFromC2);
                    }
                    else
                    {
                        if (!blockFromC2.Contains(c))
                            blockFromC2.Add(c);

                        foreach (var cell in blockFromC1)
                        {
                            if (!blockFromC2.Contains(cell))
                                blockFromC2.Add(cell);
                        }

                        foreach (var cell in blockFromC2)
                            cell.Value = c2.Value;

                        m_CellBlocks.Remove(blockFromC1);
                    }

                    m_Walls.Remove(c);
                }

                if(iteration%m_SaveIterationImage==0)
                    m_Iterations.Enqueue(m_Maze);

                stopwatchIt.Stop();
                Debug.Log($"Iteration {iteration} time: {stopwatchIt.ElapsedMilliseconds} miliseconds");

                iteration++;
            }

            stopwatch.Stop();
            double seconds = stopwatch.ElapsedMilliseconds / 1000f;
            debugMessage.Append($"Resolution time: {seconds} seconds");
            Debug.Log($"Resolution time: {seconds} seconds");

            m_Iterations.Enqueue(m_Maze);
        }

        private bool IsNotResolved()
        {
            bool hasBlockUnresolved = m_CellBlocks.Count != 1;
            bool pathExist = m_CellBlocks.Any(innerList => m_Path.All(cellModel => innerList.Contains(cellModel)));

            return hasBlockUnresolved && !pathExist;
        }

    }
}