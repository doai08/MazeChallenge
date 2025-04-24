using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualButtonControl : MonoBehaviour
{
    public Button upButton, downButton, leftButton, rightButton;
    void Start()
    {
        InitMobileControls();
    }

    void InitMobileControls()
    {
        if (upButton != null) upButton.onClick.AddListener(() => EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE, DirectionType.UP));
        if (downButton != null) downButton.onClick.AddListener(() => EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE, DirectionType.DOWN));
        if (leftButton != null) leftButton.onClick.AddListener(() => EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE, DirectionType.LEFT));
        if (rightButton != null) rightButton.onClick.AddListener(() => EventObserveManager.Instance.TriggerEvent(EventNameConst.MOVE, DirectionType.RIGHT));
    }
}
