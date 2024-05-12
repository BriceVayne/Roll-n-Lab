using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Framework
{
    internal struct S_Cell
    {
        public Vector2Int Coordinate;
        public int Value;

        public S_Cell(Vector2Int _Coordinate, int _Value)
        {
            Coordinate = _Coordinate;
            Value = _Value;
        }
    }

    internal sealed class GridGeneratorJob
    {
        private StopWatch m_Stopwatch;
        private int m_Number;

        public GridGeneratorJob(Vector2Int _Size)
        {
            m_Stopwatch = new StopWatch();

            Debug.Log($"Size [X,Y] => [ {_Size.x} , {_Size.y} ]");
            Debug.Log($"Func Name | Ticks | Milliseconds");

            Double_For_Two_Dim_Array(_Size);
            Double_For_Two_Dim_Jagged_Array(_Size);
            Single_For_Two_Dim_HashSet(_Size);
            Single_For_Two_Dim_List(_Size);
            Single_For_One_Dim_List(_Size);
            Double_For_One_Dim_Array(_Size);
            Single_For_One_Dim_Array(_Size);
            Double_For_Span(_Size);
            Single_For_Span(_Size);
            Single_For_Struct_List(_Size);
            Single_For_Dico(_Size);

            Debug.Log("\n");

            m_Stopwatch.Clear();
        }

        private void Double_For_Two_Dim_Array(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int value;
            int xMax = _Size.x;
            int yMax = _Size.y;
            int[,] maze = new int[xMax, yMax];

            for (int x = xMax - 1; x >= 0; x--)
            {
                for (int y = yMax - 1; y >= 0; y--)
                {
                    if (x == 0 || y == 0 || x == xMax - 1 || y == yMax - 1 || (x & 1) == 0 || (y & 1) == 0)
                        value = 0;
                    else
                        value = 1;

                    maze[x, y] = value;
                }
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Double_For_Two_Dim_Jagged_Array(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int value;
            int xMax = _Size.x;
            int yMax = _Size.y;
            int[][] maze = new int[xMax][];

            for (int x = xMax - 1; x >= 0; x--)
            {
                maze[x] = new int[yMax];

                for (int y = yMax - 1; y >= 0; y--)
                {
                    if (x == 0 || y == 0 || x == xMax - 1 || y == yMax - 1 || (x & 1) == 0 || (y & 1) == 0)
                        value = 0;
                    else
                        value = 1;

                    maze[x][y] = value;
                }
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Single_For_Two_Dim_HashSet(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int xMax = _Size.x;
            int yMax = _Size.y;
            HashSet<HashSet<int>> maze = new(xMax);
            HashSet<int> fullWall = new(yMax);
            HashSet<int> alternative = new(yMax);

            for (int y = yMax - 1; y >= 0; y--)
            {
                fullWall.Add(0);
                alternative.Add(y & 1);
            }

            for (int x = xMax - 1; x >= 0; x--)
            {
                if ((x & 1) == 0)
                    maze.Add(fullWall);
                else
                    maze.Add(alternative);
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Single_For_Two_Dim_List(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int xMax = _Size.x;
            int yMax = _Size.y;
            List<List<int>> maze = new(xMax);
            List<int> fullWall = new(yMax);
            List<int> alternative = new(yMax);

            for (int y = yMax - 1; y >= 0; y--)
            {
                fullWall.Add(0);
                alternative.Add(y & 1);
            }

            for (int x = xMax - 1; x >= 0; x--)
            {
                if ((x & 1) == 0)
                    maze.Add(fullWall);
                else
                    maze.Add(alternative);
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Single_For_One_Dim_List(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int xMax = _Size.x;
            int yMax = _Size.y;
            int xy = xMax * yMax;

            List<int> maze = new(xy);
            List<int> fullWall = new(yMax);
            List<int> alternative = new(yMax);

            for (int y = yMax - 1; y >= 0; y--)
            {
                fullWall.Add(0);
                alternative.Add(y & 1);
            }

            int i = xy - 1;
            while (i >= 0)
            {
                if ((i & 1) == 0)
                    maze.AddRange(fullWall);
                else
                    maze.AddRange(alternative);

                i -= xMax;
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Double_For_One_Dim_Array(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int value;
            int xMax = _Size.x;
            int yMax = _Size.y;
            int xy = xMax * yMax;
            int[] maze = new int[xy];

            for (int x = xMax - 1; x >= 0; x--)
            {
                for (int y = yMax - 1; y >= 0; y--)
                {
                    int index = x * yMax + y; // Can be better

                    if (x == 0 || y == 0 || x == xMax - 1 || y == yMax - 1 || (x & 1) == 0 || (y & 1) == 0)
                        value = 0;
                    else
                        value = 1;

                    maze[index] = value;
                }
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Single_For_One_Dim_Array(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int value;
            int xMax = _Size.x;
            int yMax = _Size.y;
            int xy = xMax * yMax;
            int[] maze = new int[xy];

            for (int i = xy - 1; i >= 0; i--)
            {
                int row = i / _Size.y; // can be better
                int col = i % _Size.y;

                if (row == 0 || col == 0 || row == xMax - 1 || col == yMax - 1 || (row & 1) == 0 || (col & 1) == 0)
                    value = 0;
                else
                    value = 1;

                maze[i] = value;
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Double_For_Span(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int value;
            int xMax = _Size.x;
            int yMax = _Size.y;
            int xy = xMax * yMax;
            Span<int> maze = new int[xy];

            for (int row = xMax - 1; row >= 0; row--)
            {
                for (int col = yMax - 1; col >= 0; col--)
                {
                    int index = row * yMax + col;

                    if (row == 0 || col == 0 || row == xMax - 1 || col == yMax - 1 || (row & 1) == 0 || (col & 1) == 0)
                        value = 0;
                    else
                        value = 1;

                    maze[index] = value;
                }
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Single_For_Span(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int value;
            int xMax = _Size.x;
            int yMax = _Size.y;
            int xy = xMax * yMax;
            Span<int> maze = new int[xy];

            for (int i = xy - 1; i >= 0; i--)
            {
                int row = i / _Size.y; // can be better
                int col = i % _Size.y;

                if (row == 0 || col == 0 || row == xMax - 1 || col == yMax - 1 || (row & 1) == 0 || (col & 1) == 0)
                    value = 0;
                else
                    value = 1;

                maze[i] = value;
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Single_For_Struct_List(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int value;
            int xMax = _Size.x;
            int yMax = _Size.y;
            List<S_Cell> maze = new(xMax * yMax);

            for (int x = xMax - 1; x >= 0; x--)
            {
                for (int y = yMax - 1; y >= 0; y--)
                {
                    Vector2Int coord = new(x, y);

                    if (x == 0 || y == 0 || x == xMax - 1 || y == yMax - 1 || (x & 1) == 0 || (y & 1) == 0)
                        value = 0;
                    else
                        value = 1;

                    maze.Add(new(coord, value));
                }
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }

        private void Single_For_Dico(Vector2Int _Size)
        {
            m_Stopwatch.StartFromMethod();

            int value;
            int xMax = _Size.x;
            int yMax = _Size.y;
            Dictionary<Vector2Int, int> maze = new(xMax * yMax);

            for (int x = xMax - 1; x >= 0; x--)
            {
                for (int y = yMax - 1; y >= 0; y--)
                {
                    Vector2Int coord = new(x, y);

                    if (x == 0 || y == 0 || x == xMax - 1 || y == yMax - 1 || (x & 1) == 0 || (y & 1) == 0)
                        value = 0;
                    else
                        value = 1;

                    maze.Add(coord, value);
                }
            }

            m_Stopwatch.StopFromMethod();

            Debug.Log(m_Stopwatch.GetTimeFromMethod());
        }
    }
}
