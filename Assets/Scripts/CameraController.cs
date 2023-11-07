using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(GameManager.MazeSize.x / 2f,Mathf.Floor(GameManager.MazeSize.y/2f ), -1f);
        Camera.main.orthographicSize = GameManager.MazeSize.y / 2f;
    }
}
