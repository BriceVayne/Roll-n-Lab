using Service;
using UnityEngine;

namespace GameControllers
{
    internal sealed class CameraController : MonoBehaviour
    {
        private void Start()
        {
            var mazeSize = GridService.Instance.MazeSize;
            transform.position = new Vector3(mazeSize.x / 2f, Mathf.Floor(mazeSize.y / 2f), -1f);
            Camera.main.orthographicSize = mazeSize.y / 2f;
        }
    }
}