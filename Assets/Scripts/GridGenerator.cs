using Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Maze
{
    public class GridGenerator : MonoBehaviour
    {
        public CellModel[,] Maze { get { return m_Maze; } }
        public Action<Queue<CellModel[,]>> OnResolutionFinished;

        private CellModel[,] m_Maze;
        private List<CellModel> m_Walls;
        private List<List<CellModel>> m_CellBlocks;
        private HashSet<CellModel> m_Path;
        private int m_Number;

        private void Start()
        {
            ResetData();
            GenerateGrid();
            DeterminePath();
            ResolvedMaze();
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.R) && GameManager.IsReadyToReload)
                ReloadGrid();
        }

        private void ResetData()
        {
            m_Maze = new CellModel[GameManager.MazeSize.x, GameManager.MazeSize.y];
            m_Walls = new List<CellModel>();
            m_CellBlocks = new List<List<CellModel>>();
            m_Path = new HashSet<CellModel>();
            m_Number = 0;
        }

        private void GenerateGrid()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int x = 0; x < m_Maze.GetLength(0); x++)
            {
                for (int y = 0; y < m_Maze.GetLength(1); y++)
                {
                    /// Set Value from position
                    int value = 0;
                    if (x == 0 || y == 0 || x == GameManager.MazeSize.x - 1 || y == GameManager.MazeSize.y - 1)
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
            Debug.Log($"Generation time: {stopwatch.ElapsedMilliseconds} miliseconds");
        }

        private void DeterminePath()
        {
            m_Path.Add(m_CellBlocks[0][0]);
            m_Path.Add(m_CellBlocks[m_CellBlocks.Count - 1][0]);
        }

        private void ResolvedMaze()
        {
            Queue<CellModel[,]> iterations = new Queue<CellModel[,]>();
            bool warHorizontal = false;
            int iteration = 0;

            iterations.Enqueue(m_Maze.Copy());

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            /// Resolved the main path
            while (IsNotResolved())
            {
                Stopwatch stopwatchIt = new Stopwatch();
                stopwatchIt.Start();

                CellModel c = m_Walls[Random.Range(0, m_Walls.Count)];
                CellModel c1, c2;

                if ((c.Position.x == 1 && c.Position.y != 1) || (c.Position.x == GameManager.MazeSize.x - 2 && c.Position.y != GameManager.MazeSize.y - 2))
                {
                    c1 = m_Maze[c.Position.x, c.Position.y - 1];
                    c2 = m_Maze[c.Position.x, c.Position.y + 1];
                }
                else if ((c.Position.y == 1 && c.Position.x != 1) || (c.Position.y == GameManager.MazeSize.y - 2))
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

                    if (iteration % GameManager.IterationInterval == 0)
                        iterations.Enqueue(m_Maze.Copy());

                    iteration++;
                }

                stopwatchIt.Stop();
                Debug.Log($"Iteration {iteration} time: {stopwatchIt.ElapsedMilliseconds} miliseconds");
            }

            /// Remove somes walls to be a complex maze
            int nbWallToBreak = 50; //TODO: define a better number
            int wallBreak = 0;

            while (wallBreak < nbWallToBreak) 
            {
                int index = Random.Range(0, m_Walls.Count);
                CellModel wallToCell = m_Walls.ElementAt(index);

                /// If the two neightboor's pair is the same number and the two numbers is different
                if(m_Maze[wallToCell.Position.x, wallToCell.Position.y - 1].Value == m_Maze[wallToCell.Position.x, wallToCell.Position.y + 1].Value &&
                   m_Maze[wallToCell.Position.x - 1, wallToCell.Position.y].Value == m_Maze[wallToCell.Position.x + 1, wallToCell.Position.y].Value &&
                   m_Maze[wallToCell.Position.x, wallToCell.Position.y - 1].Value != m_Maze[wallToCell.Position.x - 1, wallToCell.Position.y].Value)
                {
                    wallToCell.Value = m_CellBlocks[0][0].Value;
                    m_CellBlocks.Add(new List<CellModel>() { wallToCell });
                    m_Walls.RemoveAt(index);

                    wallBreak++;
                }
            }

            stopwatch.Stop();
            Debug.Log($"Resolution time: {stopwatch.ElapsedMilliseconds} milliseconds");

            iterations.Enqueue(m_Maze.Copy());

            OnResolutionFinished.Invoke(iterations);
        }

        private bool IsNotResolved()
        {
            bool hasBlockUnresolved = m_CellBlocks.Count != 1;
            bool pathExist = m_CellBlocks.Any(innerList => m_Path.All(cellModel => innerList.Contains(cellModel)));

            return hasBlockUnresolved || !pathExist;
        }

        private void ReloadGrid()
        {
            ResetData();
            GenerateGrid();
            DeterminePath();
            ResolvedMaze();
        }
    }
}