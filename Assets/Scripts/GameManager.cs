using Maze;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static Vector2Int MazeSize { get; private set; }
    public static int IterationInterval { get; private set; }
    public static bool IsReadyToReload { get; set; }
    public static HashSet<CellModel> MinimalPath { get; private set; }
    public static Stack<TwoDimensionCell> SelectedPath { get; private set; }


    [SerializeField] private Vector2Int m_Size = new Vector2Int(33,33);
    [SerializeField] [Range(1,100)] private int m_Interval = 1;

    private void Awake()
    {
        MazeSize = m_Size;
        IterationInterval = m_Interval;
        IsReadyToReload = false;
        MinimalPath = new HashSet<CellModel>();
        SelectedPath = new Stack<TwoDimensionCell>();
    }
}
