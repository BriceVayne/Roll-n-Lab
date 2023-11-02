using Maze;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        var sizeMax = Mathf.Max(GameManager.MazeSize.x, GameManager.MazeSize.y);

        transform.position = new Vector3(GameManager.MazeSize.x / 2, GameManager.MazeSize.y / 2, -sizeMax);
    }
}
