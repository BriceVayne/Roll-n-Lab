using Extension;
using Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using Stopwatch = System.Diagnostics.Stopwatch;
#endif

namespace Maze
{
    public class GridGenerator : MonoBehaviour
    {
        private Vector2Int m_MazeSize;
        private CellModel[,] m_Maze;
        private List<CellModel> m_Walls;
        private List<List<CellModel>> m_CellBlocks;
        private int m_Number;

        private void Start()
        {
            LevelManager.OnGameReload += CreateMaze;
            CreateMaze();
        }

        private void CreateMaze()
        {
            Debug.Log("Create Maze!");

            ResetData();
            GenerateGrid();
            DeterminePath();
            ResolvedMaze();
        }

        private void ResetData()
        {
            m_MazeSize = GridManager.Instance.MazeSize;
            m_Maze = new CellModel[m_MazeSize.x, m_MazeSize.y];
            m_Walls = new List<CellModel>();
            m_CellBlocks = new List<List<CellModel>>();
            m_Number = 1;

            GridManager.MinimalPath.Clear();
            LevelManager.SelectedPath.Clear();
        }

        private void GenerateGrid()
        {
#if UNITY_EDITOR
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            for (int x = 0; x < m_Maze.GetLength(0); x++)
            {
                for (int y = 0; y < m_Maze.GetLength(1); y++)
                {
                    /// Set Value from position
                    int value = 0;
                    if (x == 0 || y == 0 || x == m_MazeSize.x - 1 || y == m_MazeSize.y - 1)
                        value = -2;
                    else if (x % 2 == 0 || y % 2 == 0)
                        value = -1;
                    else
                        value = m_Number++;

                    /// Create cell and initialize it
                    m_Maze[x, y] = new CellModel(value, new Vector2Int(x, y), value == -2 ? ECellType.BORDER : value == -1 ? ECellType.WALL : ECellType.EMPTY);

                    /// Excluse internal wall
                    if (value == -1)
                        m_Walls.Add(m_Maze[x, y]);
                    else if (value >= 0)
                        m_CellBlocks.Add(new List<CellModel>() { m_Maze[x, y] });
                }
            }

#if UNITY_EDITOR
            stopwatch.Stop();
            Debug.Log($"Generation time: {stopwatch.ElapsedMilliseconds} miliseconds");
#endif
        }

        private void DeterminePath()
        {
            /// Start
            GridManager.MinimalPath.Add(m_CellBlocks[0][0]);

            /// Middle
            //TODO: define a better number
            //for (int i = 0; i < 5; i++)
            //{
            //    int rnd = Random.Range(0, m_CellBlocks.Count);
            //    GameManager.MinimalPath.Add(m_CellBlocks[rnd][0]);
            //}

            /// End
            GridManager.MinimalPath.Add(m_CellBlocks[m_CellBlocks.Count - 1][0]);
        }

        private void ResolvedMaze()
        {
            Queue<CellModel[,]> iterations = new Queue<CellModel[,]>();
            HashSet<CellModel> minimalPath = GridManager.MinimalPath;
            bool wasHorizontal = false;
            int nbWallToBreak = 10; //TODO: define a better number
            int wallBreak = 0;
#if UNITY_EDITOR
            int changedCount = 0;
#endif
            iterations.Enqueue(m_Maze.Copy());

#if UNITY_EDITOR
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            /// Resolved the main path
            while (IsNotResolved())
            {
#if UNITY_EDITOR
                Stopwatch stopwatchIt = new Stopwatch();
                stopwatchIt.Start();
#endif
                CellModel c = m_Walls[Random.Range(0, m_Walls.Count)];
                CellModel c1, c2;

                /// Find neighboor
                if ((c.Position.x == 1 && c.Position.y != 1) || (c.Position.x == m_MazeSize.x - 2 && c.Position.y != m_MazeSize.y - 2))
                {
                    /// Left & Right Border
                    c1 = m_Maze[c.Position.x, c.Position.y - 1];
                    c2 = m_Maze[c.Position.x, c.Position.y + 1];
                }
                else if ((c.Position.y == 1 && c.Position.x != 1) || (c.Position.y == m_MazeSize.y - 2))
                {
                    /// Up & Down Border
                    c1 = m_Maze[c.Position.x - 1, c.Position.y];
                    c2 = m_Maze[c.Position.x + 1, c.Position.y];
                }
                else
                {
                    /// Middle Maze
                    if (wasHorizontal)
                    {
                        c1 = m_Maze[c.Position.x, c.Position.y - 1];
                        c2 = m_Maze[c.Position.x, c.Position.y + 1];
                    }
                    else
                    {
                        c1 = m_Maze[c.Position.x - 1, c.Position.y];
                        c2 = m_Maze[c.Position.x + 1, c.Position.y];
                    }

                    wasHorizontal = !wasHorizontal;
                }

                /// Find cell's blocks - Find a changed
                if (c1.Value != -1 && c2.Value != -1 && c1.Value != c2.Value)
                {
                    List<CellModel> blockFromC1 = m_CellBlocks.Find(i => i.Contains(c1));
                    List<CellModel> blockFromC2 = m_CellBlocks.Find(i => i.Contains(c2));

                    /// Move the minus list into the greatest
                    if (blockFromC1.Count >= blockFromC2.Count)
                    {
                        if (!blockFromC1.Contains(c))
                            blockFromC1.Add(c);

                        foreach (var cell in blockFromC2)
                        {
                            if (!blockFromC1.Contains(cell))
                                blockFromC1.Add(cell);
                        }

                        foreach (var cell in blockFromC1)
                        {
                            cell.Value = c1.Value;
                            cell.Type = c1.Type;
                        }

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
                        {
                            cell.Value = c2.Value;
                            cell.Type = c2.Type;
                        }

                        m_CellBlocks.Remove(blockFromC1);
                    }

                    m_Walls.Remove(c);

                    /// Save iteration from the according percentage
                    if (GridManager.Instance.GenerationIntervalPercentage != EIntervalPercentage.NONE)
                    {
                        int percentage = 100 / (int)GridManager.Instance.GenerationIntervalPercentage;
                        bool saveChanged = changedCount % percentage == 0;
                        if (changedCount % percentage == 0)
                            iterations.Enqueue(m_Maze.Copy());
                    }
#if UNITY_EDITOR
                    changedCount++;
#endif
                }
#if UNITY_EDITOR
                stopwatchIt.Stop();
                Debug.Log($"Iteration {changedCount} time: {stopwatchIt.ElapsedMilliseconds} miliseconds");
#endif
            }
#if UNITY_EDITOR
            Debug.Log($"Walls : {m_Walls.Count}");
#endif
            while (wallBreak < nbWallToBreak)
            {
                int index = Random.Range(0, m_Walls.Count);
                CellModel wallToCell = m_Walls.ElementAt(index);

                /// If the two neightboor's pair is the same number and the two numbers is different
                if (m_Maze[wallToCell.Position.x, wallToCell.Position.y - 1].Value == m_Maze[wallToCell.Position.x, wallToCell.Position.y + 1].Value &&
                   m_Maze[wallToCell.Position.x - 1, wallToCell.Position.y].Value == m_Maze[wallToCell.Position.x + 1, wallToCell.Position.y].Value &&
                   m_Maze[wallToCell.Position.x, wallToCell.Position.y - 1].Value != m_Maze[wallToCell.Position.x - 1, wallToCell.Position.y].Value)
                {
                    wallToCell.Value = m_CellBlocks[0][0].Value;
                    wallToCell.Type = ECellType.EMPTY;
                    m_CellBlocks[0].Add(wallToCell);
                    m_Walls.RemoveAt(index);

                    wallBreak++;
                }
            }

            //TODO : reapply type after changing cell - Find a better way
            for (int i = 0; i < minimalPath.Count; i++)
            {
                if (i == 0)
                    minimalPath.ElementAt(i).Type = ECellType.START;
                else if (i == minimalPath.Count - 1)
                    minimalPath.ElementAt(i).Type = ECellType.END;
                else
                    minimalPath.ElementAt(i).Type = ECellType.PATH;

                minimalPath.ElementAt(i).Value = 0;
            }

#if UNITY_EDITOR
            stopwatch.Stop();
            Debug.Log($"Resolution time: {stopwatch.ElapsedMilliseconds} milliseconds");
#endif

            iterations.Enqueue(m_Maze.Copy());

            GridManager.OnGenerationFinished.Invoke(iterations);
        }

        private bool IsNotResolved()
        {
            bool hasBlockUnresolved = m_CellBlocks.Count != 1;
            bool pathExist = m_CellBlocks.Any(innerList => GridManager.MinimalPath.All(cellModel => innerList.Contains(cellModel)));

            return hasBlockUnresolved || !pathExist;
        }
    }
}