using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class TwoDimensionGeneration : MonoBehaviour
    {
        [SerializeField] private GridGenerator m_Generator;
        [SerializeField] private TwoDimensionCell m_Prefab;
        [SerializeField] private Transform m_Content;
        [SerializeField] private float m_IterationTime;

        private TwoDimensionCell[,] m_Grid;
        private Queue<CellModel[,]> m_Iterations;
        private float m_Time;
        private bool m_IsFinished;

        private void Awake()
        {
            m_Time = 0f;
            m_IsFinished = false;
            m_Generator.OnResolutionFinished += GenerateGrid;
        }

        private void Update()
        {
            if (!m_IsFinished)
            {
                m_Time += Time.deltaTime;
                if (m_Time >= m_IterationTime)
                {
                    if (m_Iterations != null && m_Iterations.Count > 0)
                    {
                        if (m_Iterations.TryDequeue(out var iteration))
                        {
                            for (int x = 0; x < GameManager.MazeSize.x; x++)
                                for (int y = 0; y < GameManager.MazeSize.y; y++)
                                    m_Grid[x, y].UpdateDisplay(iteration[x, y].Value);
                        }
                    }
                    else if (m_Iterations != null && m_Iterations.Count == 0)
                    {
                        m_IsFinished = true;
                        GameManager.IsReadyToReload = true;
                    }

                    m_Time = 0f;
                }
            }
        }

        private void GenerateGrid(Queue<CellModel[,]> iterations)
        {
            m_Iterations = iterations;

            if (m_Grid == null)
            {
                m_Grid = new TwoDimensionCell[GameManager.MazeSize.x, GameManager.MazeSize.y];

                if (m_Iterations.TryDequeue(out var iteration))
                {
                    for (int x = 0; x < GameManager.MazeSize.x; x++)
                    {
                        for (int y = 0; y < GameManager.MazeSize.y; y++)
                        {
                            m_Grid[x, y] = Instantiate(m_Prefab, m_Content);
                            m_Grid[x, y].Initialize(iteration[x, y].Value, iteration[x, y].Position);
                        }
                    }
                }
            }
            else
            {
                if (m_Iterations.TryDequeue(out var iteration))
                {
                    for (int x = 0; x < GameManager.MazeSize.x; x++)
                    {
                        for (int y = 0; y < GameManager.MazeSize.y; y++)
                        {
                            m_Grid[x, y].UpdateDisplay(iteration[x, y].Value);
                        }
                    }
                }
            }
        }
    }
}