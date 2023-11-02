using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(GameManager.MazeSize.x / 2, GameManager.MazeSize.y / 2, -GameManager.MazeSize.y);
    }
}
