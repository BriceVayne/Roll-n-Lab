
public class GameManager : Singleton<GameManager>
{
    public static int MazeSize { get { return m_MazeSize; } }
    public static int IterationInterval { get { return m_IterationInterval; } }

    private static int m_MazeSize = 33;
    private static int m_IterationInterval = 1;
}
