using Maze;
using Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public delegate void GenerationFinishedDelegate(Queue<CellModel[,]> queue);
    public delegate void GameWinDelegate();
    public delegate void GameOverDelegate();
    public delegate void GameReloadDelegate();
    public delegate void CellCreatedDelegate(CellModel cell);
    public delegate void CellUpdateDelegate(CellModel cell);

    public static GenerationFinishedDelegate OnGenerationFinished;
    public static GameWinDelegate OnGameWin;
    public static GameOverDelegate OnGameOver;
    public static GameReloadDelegate OnGameReload;
    public static CellCreatedDelegate OnCellCreated;
    public static CellUpdateDelegate OnCellUpdated;

    public static bool IsReadyToReload { get; set; }
    public static Vector2Int MazeSize { get; private set; }
    public static int IterationInterval { get; private set; }
    public static HashSet<CellModel> MinimalPath { get; private set; }
    public static Stack<TwoDimensionCell> SelectedPath { get; private set; }


    [SerializeField] private Vector2Int m_Size = new Vector2Int(33,33);
    [SerializeField] [Range(1,100)] private int m_Interval = 1;

    private void Awake()
    {
        OnGameWin = null;
        OnGameOver = null;
        OnGameReload = null;

        IsReadyToReload = false;

        MazeSize = m_Size;
        IterationInterval = m_Interval;
        MinimalPath = new HashSet<CellModel>();
        SelectedPath = new Stack<TwoDimensionCell>();
    }

    private void Start()
    {
        OnGameReload += ResetRealoadBoolean;
    }

    private void ResetRealoadBoolean()
        => IsReadyToReload = false;
}
