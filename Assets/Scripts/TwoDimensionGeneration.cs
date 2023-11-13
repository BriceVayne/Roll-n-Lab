using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Maze
{
    public class TwoDimensionGeneration : MonoBehaviour
    {
        [SerializeField] private GridGenerator m_Generator;
        [SerializeField] private TwoDimensionCell m_Prefab;
        [SerializeField] private Transform m_Content;
        [SerializeField] private float m_IterationTime;

        private Action<bool> m_ToggleInfos;
        private TwoDimensionCell[,] m_Grid;
        private Queue<CellModel[,]> m_Iterations;
        private bool m_IsFinished;
        private float m_Time;

        private void Awake()
        {
            m_Generator.OnResolutionFinished += GenerateGrid;
            m_ToggleInfos = null;
            m_Iterations = new Queue<CellModel[,]>();
            m_IsFinished = false;
            m_Time = 0f;
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
                                    m_Grid[x, y].UpdateDisplay(iteration[x, y].Value, iteration[x, y].Type);
                        }
                    }
                    else if (m_Iterations != null && m_Iterations.Count == 0)
                    {
                        m_IsFinished = true;
                        GameManager.IsReadyToReload = true;

                        Vector2Int startPos = GameManager.MinimalPath.ElementAt(0).Position;
                        GameManager.SelectedPath.Push(m_Grid[startPos.x, startPos.y]);

                        m_ToggleInfos.Invoke(false);
                    }

                    m_Time = 0f;
                }
            }

            if (m_IsFinished && Input.GetKeyDown(KeyCode.Space))
                m_ToggleInfos.Invoke(true);
            else if(m_IsFinished && Input.GetKeyUp(KeyCode.Space))
                m_ToggleInfos.Invoke(false);

        }

        private void GenerateGrid(Queue<CellModel[,]> iterations)
        {
            m_Iterations.Clear();
            m_Iterations = iterations;

            m_IsFinished = false;
            m_Time = 0f;

            if(m_Iterations.TryDequeue(out var iteration))
            {
                if (m_Grid == null)
                    m_Grid = CreateGrid(iteration);
                else
                {
                    for (int x = 0; x < m_Grid.GetLength(0); x++)
                        for (int y = 0; y < m_Grid.GetLength(1); y++)
                            m_Grid[x, y].UpdateDisplay(iteration[x, y].Value, iteration[x, y].Type);
                }
            }
        }

        private TwoDimensionCell[,] CreateGrid(CellModel[,] models)
        {
            var grid = new TwoDimensionCell[models.GetLength(0), models.GetLength(1)];

            for (int x = 0; x < models.GetLength(0); x++)
            {
                for (int y = 0; y < models.GetLength(1); y++)
                {
                    grid[x, y] = Instantiate(m_Prefab, m_Content);
                    grid[x, y].Initialize(models[x, y].Value, models[x, y].Position, models[x,y].Type);

                    m_ToggleInfos += grid[x, y].ToggleInfo;
                }
            }

            return grid;
        }
    }
}