
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class BackGround : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Camera _camera;
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        BgAdaptive();
    }

    public void BgAdaptive()
    {
        float cameraHeight = _camera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Screen.width / Screen.height;

        Vector2 spriteSize = _renderer.sprite.bounds.size;

        float scaleX = cameraWidth / spriteSize.x;
        float scaleY = cameraHeight / spriteSize.y;

        // Scale theo chiều nào lớn hơn để đảm bảo phủ hết
        float finalScale = Mathf.Max(scaleX, scaleY);
        transform.localScale = Vector3.one * finalScale;
         transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    }
}
