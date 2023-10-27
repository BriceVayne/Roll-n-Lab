using Maze;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GridGenerator m_Grid;

    private void Start()
    {
        transform.position = new Vector3(m_Grid.Size / 2, m_Grid.Size / 2, -m_Grid.Size);
    }
}
