using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDCtrl : MonoBehaviour
{
    public Button pauseBtn;
    public Button showPathBtn;
    public Button autoMoveBtn;

    public TextMeshProUGUI stageTxt;

    void Start()
    {
        if (pauseBtn != null) pauseBtn.onClick.AddListener(OnClickPause);
        if (showPathBtn != null) showPathBtn.onClick.AddListener(OnClickShowPath);
         if (autoMoveBtn != null) autoMoveBtn.onClick.AddListener(OnAutoMove);
         EventObserveManager.Instance.Subscribe(EventNameConst.LOAD_STAGE,LoadStage);
         SetTopBarData(StageManager.Instance.currentStage);
    }
    void OnDestroy()
    {
        EventObserveManager.Instance.Unsubscribe(EventNameConst.LOAD_STAGE,LoadStage);
    }
    void LoadStage(object data)
    {
        StageItemData currentStage = data as StageItemData;
        SetTopBarData(currentStage);
    }
    void SetTopBarData(StageItemData _currentStage)
    {

        stageTxt.text = $"STAGE {_currentStage.stage}";
    }
    public void OnClickPause()
    {
        UIManager.Instance.ShowPopup(PopUpPathConst.POPUP_PAUSE);
    }
    public void OnClickShowPath()
    {
        EventObserveManager.Instance.TriggerEvent(EventNameConst.SHOW_PATH);
    }
      public void OnAutoMove()
    {
        EventObserveManager.Instance.TriggerEvent(EventNameConst.AUTO_MOVE);
    }
}
