using UnityEngine;

public class SafeAreaPhone : MonoBehaviour
{
    public RectTransform uiContainer;

    void Start()
    {
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        // Lấy thông tin safe area của màn hình
        Rect safeArea = Screen.safeArea;
        
        // Điều chỉnh kích thước và vị trí của UI theo Safe Area
        Vector2 anchorMin = new Vector2(safeArea.x / Screen.width, safeArea.y / Screen.height);
        Vector2 anchorMax = new Vector2((safeArea.x + safeArea.width) / Screen.width, 
                                        (safeArea.y + safeArea.height) / Screen.height);

        uiContainer.anchorMin = anchorMin;
        uiContainer.anchorMax = anchorMax;

        // Đảm bảo vị trí của UI luôn chính xác trong vùng Safe Area
        uiContainer.offsetMin = Vector2.zero;
        uiContainer.offsetMax = Vector2.zero;
    }
}