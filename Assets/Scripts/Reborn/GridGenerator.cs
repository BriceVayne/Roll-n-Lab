using Reborn.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Reborn.Generator
{
    internal sealed class GridGenerator
    {
        private StopWatch m_Stopwatch;
        private Vector2Int m_MazeSize;

        private CellModel[,] m_Maze;
        private List<CellModel> m_Walls;
        private List<List<CellModel>> m_CellBlocks;
        private HashSet<CellModel> m_MinimalPath;
        private List<CellModel[,]> m_Iterations;

        private int m_Number;
        private int m_NbWallToBreak = 10; //TODO: define a better number
        private int m_ChangedCount = 0;

        public GridGenerator(Vector2Int _MazeSize)
        {
            m_Stopwatch = new StopWatch();
            m_MazeSize = _MazeSize;

            Regenerate();
        }

        public void Regenerate()
        {
            m_Stopwatch.Reset();
            m_Stopwatch.Start();

            ResetData();
            GenerateGrid();
            DeterminePath();
            ResolvedMaze();

            m_Stopwatch.Stop();
        }

        private void ResetData()
        {
            m_Stopwatch.StartFromMethod();

            m_Maze = new CellModel[m_MazeSize.x, m_MazeSize.y];
            m_Walls = new List<CellModel>();
            m_CellBlocks = new List<List<CellModel>>();
            m_Iterations = new List<CellModel[,]>();
            m_Number = 1;
            m_ChangedCount = 0;

            m_Stopwatch.StopFromMethod();
        }

        private void GenerateGrid()
        {
            m_Stopwatch.StartFromMethod();

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

                    /// Set Type from value
                    ECellType type = value == -2 ?
                                     ECellType.BORDER : value == -1 ?
                                     ECellType.WALL : ECellType.EMPTY;

                    /// Create cell
                    m_Maze[x, y] = new CellModel(value, new Vector2Int(x, y), type);

                    /// Sort wall and empty cell
                    if (type == ECellType.WALL)
                        m_Walls.Add(m_Maze[x, y]);
                    else if (type == ECellType.EMPTY)
                        m_CellBlocks.Add(new List<CellModel>() { m_Maze[x, y] });
                }
            }

            m_Stopwatch.StopFromMethod();
        }

        private void DeterminePath()
        {
            m_Stopwatch.StartFromMethod();

            var randomIndex = Random.Range(0, m_CellBlocks.Count);
            var cellStart = m_CellBlocks[randomIndex][0];
            CellModel cellEnd = null;

            /// Start
            m_MinimalPath.Add(cellStart);

            float maxDistance = 0f;

            /// Find the highest distance between start and end
            for (int i = 0; i < m_CellBlocks.Count; i++)
            {
                if (i == randomIndex)
                    continue;

                var cell = m_CellBlocks[i][0];
                var distance = Vector2Int.Distance(cellStart.Position, cell.Position);

                if (distance > maxDistance)
                {
                    cellEnd = cell;
                    maxDistance = distance;
                }
            }

            if (cellEnd == null)
                throw new NullReferenceException("Cannot find end cell");

            /// End
            m_MinimalPath.Add(cellEnd);

            m_Stopwatch.StopFromMethod();
        }

        private void ResolvedMaze()
        {
            m_Stopwatch.StartFromMethod();

            HashSet<CellModel> minimalPath = m_MinimalPath;

            bool wasHorizontal = false;
            int wallBreak = 0;

            /// Add first iteration
            m_Iterations.Add(m_Maze.Copy());

            /// Resolved the main path
            while (IsNotResolved())
            {
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
                            cell.SetValue(c1.Value);

                            if (cell.Type == ECellType.WALL)
                                cell.SetType(ECellType.EMPTY);
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
                            cell.SetValue(c2.Value);

                            if (cell.Type == ECellType.WALL)
                                cell.SetType(ECellType.EMPTY);
                        }

                        m_CellBlocks.Remove(blockFromC1);
                    }

                    m_Walls.Remove(c);

                    m_Iterations.Add(m_Maze.Copy());

                    m_ChangedCount++;
                }
            }

            while (wallBreak < m_NbWallToBreak)
            {
                int index = Random.Range(0, m_Walls.Count);
                CellModel wallToCell = m_Walls.ElementAt(index);

                /// If the two neightboor's pair is the same number and the two numbers is different
                if (m_Maze[wallToCell.Position.x, wallToCell.Position.y - 1].Value == m_Maze[wallToCell.Position.x, wallToCell.Position.y + 1].Value &&
                   m_Maze[wallToCell.Position.x - 1, wallToCell.Position.y].Value == m_Maze[wallToCell.Position.x + 1, wallToCell.Position.y].Value &&
                   m_Maze[wallToCell.Position.x, wallToCell.Position.y - 1].Value != m_Maze[wallToCell.Position.x - 1, wallToCell.Position.y].Value)
                {
                    wallToCell.SetValue(m_CellBlocks[0][0].Value);
                    wallToCell.SetType(ECellType.EMPTY);
                    m_CellBlocks[0].Add(wallToCell);
                    m_Walls.RemoveAt(index);

                    wallBreak++;
                }
            }

            m_Iterations.Add(m_Maze.Copy());
            m_Stopwatch.StopFromMethod();
        }

        private bool IsNotResolved()
        {
            m_Stopwatch.StartFromMethod();

            bool hasBlockUnresolved = m_CellBlocks.Count != 1;
            bool pathExist = m_CellBlocks.Any(innerList => m_MinimalPath.All(cellModel => innerList.Contains(cellModel)));

            m_Stopwatch.StopFromMethod();

            return hasBlockUnresolved || !pathExist;
        }
    }
}
