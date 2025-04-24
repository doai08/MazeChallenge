using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    private int boardWidth = BoardConst.WIDTH;
    private int boardHeight = BoardConst.HEIGHT;

    [SerializeField] private float offsetZ = -10f;
    [SerializeField] private float padding = 0.5f;

    void Start()
    {
        float centerX = (boardWidth - 1) / 2f;
        float centerY = (boardHeight - 1) / 2f;
        transform.position = new Vector3(centerX, centerY, offsetZ);

        float screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = boardHeight / 2f;
        float cameraWidth = boardWidth / (2f * screenAspect);

        //max đảm bảo camera thấy toàn bộ cả chiều rộng & cao
        Camera.main.orthographicSize = Mathf.Max(cameraHeight, cameraWidth) + padding;
    }
}
