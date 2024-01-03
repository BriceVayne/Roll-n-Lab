using Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class GridManager : Manager<GridManager>
    {
        public Action<CellModel> OnCellCreated;
        public Action<CellModel> OnCellUpdated;
        public Action<CellModel> OnCellDeleted;

        public bool CanReloadMaze { get; }
        public Vector2Int MazeSize { get { return m_Size; } }
        public EIntervalPercentage GenerationIntervalPercentage { get { return m_GenerationIntervalPercentage; } }
        public List<CellModel[,]> SnapShots { get; set; }
        public HashSet<CellModel> MinimalPath { get; private set; }

        [Header("Debug Infos")]
        [SerializeField] private Vector2Int m_Size = new Vector2Int(33, 33);
        [SerializeField] private EIntervalPercentage m_GenerationIntervalPercentage = EIntervalPercentage.ALL;

        protected override void AwakeBehaviour()
        {
            OnCellCreated = null;
            OnCellUpdated = null;
            OnCellDeleted = null;

            MinimalPath = new HashSet<CellModel>();
        }

        protected override void StartBehaviour()
        {
        }
    }
}