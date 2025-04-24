using UnityEngine;

public class KeyBoardControl : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE, DirectionType.RIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE, DirectionType.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE, DirectionType.UP);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE, DirectionType.DOWN);
            }
        }
    }
}
