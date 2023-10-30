using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static int MazeSize { get; private set; }
    public static int IterationInterval { get; private set; }

    [SerializeField] private int m_Size = 33;
    [SerializeField] [Range(1,100)] private int m_Interval = 1;

    private void Awake()
    {
        MazeSize = m_Size;
        IterationInterval = m_Interval;
    }
}
