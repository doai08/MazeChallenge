using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    public float minSwipeDistance = 50f; // khoảng cách tối thiểu để tính là swipe

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private bool isSwiping;

    public enum SwipeDirection { None, Up, Down, Left, Right }
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            startTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            endTouchPos = Input.mousePosition;
            DetectSwipe();
            isSwiping = false;
        }
    }

    void DetectSwipe()
    {
        Vector2 delta = endTouchPos - startTouchPos;

        if (delta.magnitude < minSwipeDistance)
        {
            return;
        }

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if(delta.x > 0)
            {
                EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE,DirectionType.RIGHT);
            }
            else
            {
                EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE,DirectionType.LEFT);
            }
        }
        else
        {
            if(delta.y > 0)
            {
                EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE,DirectionType.UP);
            }
            else
            {
                EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE,DirectionType.DOWN);
            }
        }
    }
}
