using Maze;
using UnityEngine;

namespace GameControllers
{
    public class CameraController : MonoBehaviour
    {
        private void Start()
        {
            var mazeSize = GridManager.Instance.MazeSize;
            transform.position = new Vector3(mazeSize.x / 2f, Mathf.Floor(mazeSize.y / 2f), -1f);
            Camera.main.orthographicSize = mazeSize.y / 2f;
        }
    }
}