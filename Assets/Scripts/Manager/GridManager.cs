using Layers;
using Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class GridManager : Singleton<GridManager>, IManager
    {
        public static Action<Queue<CellModel[,]>> OnGenerationFinished;
        public static Action<CellModel> OnCellCreated;
        public static Action<CellModel> OnCellUpdated;
        public static Action<CellModel> OnCellDeleted;

        public bool IsReady { get; private set; }
        public bool CanReloadMaze { get; }
        public Vector2Int MazeSize { get { return m_Size; } }
        public EIntervalPercentage GenerationIntervalPercentage { get { return m_GenerationIntervalPercentage; } }
        public static HashSet<CellModel> MinimalPath { get; private set; }

        [Header("Debug Infos")]
        [SerializeField] private Vector2Int m_Size = new Vector2Int(33, 33);
        [SerializeField] private EIntervalPercentage m_GenerationIntervalPercentage = EIntervalPercentage.ALL;

        private CellModel[,] m_InternalGrid;
        private Queue<CellModel[,]> m_InternalSnapShots;

        private void Awake()
        {
            IsReady = false;

            OnGenerationFinished = null;
            OnCellCreated = null;
            OnCellUpdated = null;
            OnCellDeleted = null;

            OnGenerationFinished += CreateOrUpdateGrid;
            MinimalPath = new HashSet<CellModel>();

            Debug.Log("Grid Manager Awake");
        }

        private void Start()
        {
            IsReady = true;
        }

        private void CreateOrUpdateGrid(Queue<CellModel[,]> _GridSnapShots)
        {
            m_InternalSnapShots = _GridSnapShots;

            StartCoroutine(WaitManager());

            
        }

        private void CreateGrid(CellModel[,] _SnapShot)
        {
            m_InternalGrid = new CellModel[m_Size.x, m_Size.y];
            for (int x = 0; x < m_InternalGrid.GetLength(0); x++)
            {
                for (int y = 0; y < m_InternalGrid.GetLength(1); y++)
                {
                    m_InternalGrid[x, y] = _SnapShot[x, y];
                    OnCellCreated.Invoke(_SnapShot[x, y]);
                }
            }
        }

        private void UpdateGrid(CellModel[,] _SnapShot)
        {
            for (int x = 0; x < m_InternalGrid.GetLength(0); x++)
            {
                for (int y = 0; y < m_InternalGrid.GetLength(1); y++)
                {
                    m_InternalGrid[x, y] = _SnapShot[x, y];
                    OnCellUpdated.Invoke(_SnapShot[x, y]);
                }
            }
        }

        private IEnumerator WaitManager()
        {
            yield return new WaitWhile(() => !LayerManager.Instance.IsReady);

            CreateGrid(m_InternalSnapShots.Dequeue());
        }
    }
}