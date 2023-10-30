using Maze;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(GameManager.MazeSize / 2, GameManager.MazeSize / 2, -GameManager.MazeSize);
    }
}
