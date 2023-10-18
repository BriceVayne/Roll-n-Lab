using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int Size { get { return m_MazeSize; } }

    [SerializeField] private int m_MazeSize = 32;
    [SerializeField] private Cell m_Prefab;

    private Cell[,] m_Cells;
    private List<Cell> m_Walls;

    private void Start()
    {
        GenerateGrid();
        PlaceStartAndEnd();
        UpdateMaze();
    }

    private void GenerateGrid()
    {
        int nb = 1;

        m_Cells = new Cell[m_MazeSize, m_MazeSize];
        m_Walls = new List<Cell>();

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
                    value = nb++;

                /// Generate color from position
                Color color;
                if (value == -1 || value == -2)
                    color = Color.black;
                else
                    color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f);

                /// Create cell and initialize it
                CellModel model = new CellModel(value, color, new Vector2(x, y));
                m_Cells[x, y] = Instantiate(m_Prefab, transform);
                m_Cells[x, y].InitializeData(model);

                /// Excluse internal wall
                if (value == -1)
                    m_Walls.Add(m_Cells[x, y]);
            }
        }
    }

    private void PlaceStartAndEnd()
    {
        int rnd;
        Cell cell;

        rnd = Random.Range(0, 4);
        cell = m_Walls[rnd];
        cell.Value = 0;
        cell.Color = Color.white;

        m_Walls.Remove(cell);

        rnd = Random.Range(m_Walls.Count - 4, m_Walls.Count);
        cell = m_Walls[rnd];
        cell.Value = 0;
        cell.Color = Color.white;

        m_Walls.Remove(cell);
    }

    private void UpdateMaze()
    {
        for (int x = 0; x < m_MazeSize; x++)
        {
            for (int y = 0; y < m_MazeSize; y++)
            {
                m_Cells[x, y].UpdateData();
                m_Cells[x, y].UpdateName();
            }
        }
    }

    private IEnumerator ResolvedMaze()
    {
        /// Take a % of walls
        float percentage = 0.5f;
        int randomWalls = (int)Mathf.Abs(m_Walls.Count * percentage);

        for (int i = 0; i < randomWalls; i++)
        {
            Cell c = m_Walls[Random.Range(0, m_Walls.Count)];
        }


        //int x = Random.Range(1, (m_MazeSize - 2) + 1);
        //int y;

        //if (x % 2 == 0)
        //    y = Random.Range(1, ((m_MazeSize - 1) / 2) * 2 + 1);
        //else
        //    y = Random.Range(2, ((m_MazeSize - 2) / 2) * 2 + 2);

        //Cell c1, c2;

        //if (m_Cells[x, y].Value == -1)
        //{
        //    c1 = m_Cells[x, y - 1];
        //    c2 = m_Cells[x, y + 1];
        //}
        //else
        //{
        //    c1 = m_Cells[x - 1, y];
        //    c2 = m_Cells[x + 1, y];
        //}

        //if (c1.Value != c2.Value)
        //{
        //    m_Cells[x, y].Value = 0;

        //    for (int i = 1; i < m_MazeSize - 1; i += 2)
        //    {
        //        for (int j = 1; j < m_MazeSize - 1; j += 2)
        //        {
        //            if (m_Cells[x, y].Value == c2.Value)
        //                m_Cells[x, y].Value = c1.Value;
        //        }
        //    }
        //}

        yield return new WaitForSeconds(2f);
    }
}