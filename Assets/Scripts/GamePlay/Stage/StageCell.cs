
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageCell : MonoBehaviour
{
    [SerializeField] List<GameObject> stars;

    [SerializeField] private GameObject gridLock;
    [SerializeField] private GameObject tutorial;

    [SerializeField] private GameObject topLine;

    [SerializeField] private GameObject leftLine;

    private StageItemView stageItemView;
    [SerializeField] private TextMeshProUGUI stageTxt;


    public void SetData(int dataIndex, StageItemView _stageItemViewData, bool isFirstItem, bool isEndItem, bool isEvenCell)

    {
        stageItemView = _stageItemViewData;

        SetStageTxt();
        SetStar();
        SetTutorial();
        SetLine(isFirstItem, isEndItem, isEvenCell);

        SetGridLock(_stageItemViewData);
    }

    private void SetStageTxt()
    {
        stageTxt.text = $"{stageItemView.stage}";
    }
    private void SetStar()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (i < stageItemView.star)
            {
                stars[i].SetActive(true);
            }
            else
            {
                stars[i].SetActive(false);
            }
        }
    }
    private void SetTutorial()
    {

        if (stageItemView.stage == 1)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }
    }
    private void SetGridLock(StageItemView _stageItemViewData)
    {
        gridLock.SetActive(!_stageItemViewData.unlocked);
    }
    private void SetLine(bool isFirstItem, bool isEndItem, bool isEvenCell)
    {
        // === TOP LINE ===
        bool topLineState = isEndItem;
        topLine.SetActive(topLineState);

        // === LEFT LINE ===
        bool leftLineState;

        if (isEvenCell)
        {
            if (isFirstItem)
                leftLineState = false;  
            else if (isEndItem)
                leftLineState = true;    
            else
                leftLineState = true;    
        }
        else
        {
            if (isFirstItem)
                leftLineState = true;    
            else if (isEndItem)
                leftLineState = false;   
            else
                leftLineState = true;    
        }

        leftLine.SetActive(leftLineState);
    }



    public void OnClickStage()
    {
        if (stageItemView.unlocked)
        {
            StageManager.Instance.LoadStage(stageItemView.stage);
        }
    }
}
