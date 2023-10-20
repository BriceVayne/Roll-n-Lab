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
    private List<List<Cell>> m_Paths;

    private void Start()
    {
        GenerateGrid();
        //PlaceStartAndEnd();
        UpdateMaze();

        StartCoroutine(ResolvedMaze());
    }

    private void GenerateGrid()
    {
        int nb = 1;

        m_Cells = new Cell[m_MazeSize, m_MazeSize];
        m_Walls = new List<Cell>();
        m_Paths = new List<List<Cell>>();

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
                CellModel model = new CellModel(value, color, new Vector2Int(x, y));
                m_Cells[x, y] = Instantiate(m_Prefab, transform);
                m_Cells[x, y].InitializeData(model);

                /// Excluse internal wall
                if (value == -1)
                    m_Walls.Add(m_Cells[x, y]);
                else if (value >= 1)
                    m_Paths.Add(new List<Cell>() { m_Cells[x, y] });
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

        m_Paths.Add(new List<Cell>() { cell });
        m_Walls.Remove(cell);

        rnd = Random.Range(m_Walls.Count - 4, m_Walls.Count);
        cell = m_Walls[rnd];
        cell.Value = 0;
        cell.Color = Color.white;

        m_Paths.Add(new List<Cell>() { cell });
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
        bool warHorizontal = false;
        while (m_Paths.Count != 1)
        {
            Cell c = m_Walls[Random.Range(0, m_Walls.Count)];
            Debug.Log($"Wall : {c.Position}");
            Cell c1, c2;

            if ((c.Position.x == 1 && c.Position.y != 1) || (c.Position.x == m_MazeSize - 2 && c.Position.y != m_MazeSize - 2))
            {
                c1 = m_Cells[c.Position.x, c.Position.y - 1];
                c2 = m_Cells[c.Position.x, c.Position.y + 1];
            }
            else if ((c.Position.y == 1 && c.Position.x != 1) || (c.Position.y == m_MazeSize - 2))
            {
                c1 = m_Cells[c.Position.x - 1, c.Position.y];
                c2 = m_Cells[c.Position.x + 1, c.Position.y];
            }
            else
            {
                if (warHorizontal)
                {
                    c1 = m_Cells[c.Position.x, c.Position.y - 1];
                    c2 = m_Cells[c.Position.x, c.Position.y + 1];
                }
                else
                {
                    c1 = m_Cells[c.Position.x - 1, c.Position.y];
                    c2 = m_Cells[c.Position.x + 1, c.Position.y];
                }

                warHorizontal = !warHorizontal;
            }

            Debug.Log($"C1 : {c1.Value} | C2 : {c2.Value}");

            if ((c1.Value != -1 && c2.Value != -1) && c1.Value != c2.Value)
            {
                List<Cell> blockFromC1 = m_Paths.Find(i => i.Contains(c1));
                List<Cell> blockFromC2 = m_Paths.Find(i => i.Contains(c2));

                List<Cell> pathWithC1 = new List<Cell>(blockFromC1);
                List<Cell> pathWithC2 = new List<Cell>(blockFromC2);

                m_Paths.Remove(blockFromC1);
                m_Paths.Remove(blockFromC2);

                if (!pathWithC1.Contains(c))
                    pathWithC1.Add(c);

                pathWithC2.ForEach(i =>
                {
                    if (!pathWithC1.Contains(i))
                        pathWithC1.Add(i);
                });

                pathWithC1.ForEach(i => i.UpdateData(c1));

                m_Paths.Add(pathWithC1);

                m_Walls.Remove(c);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}