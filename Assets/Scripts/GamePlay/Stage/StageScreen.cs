using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class StageScreen : MonoBehaviour, IEnhancedScrollerDelegate
{

    [SerializeField] EnhancedScroller scroller;
    private UserData userData;


    [SerializeField] private EnhancedScrollerCellView rowCellViewPrefab;

    private int numberItemInRow = 4;

    List<StageItemView> stageItemViews;


    void Start()
    {
        userData = DataInGameManager.Instance.GetUserData();
        Application.targetFrameRate = 60;
        scroller.Delegate = this;
        LoadData();
        scroller.ReloadData();

    }
    void LoadData()
    {   
        stageItemViews = new List<StageItemView>();
        List<StageItemData> stageItemDatas = userData.passedStages;
        foreach (var stageData in stageItemDatas)
        {
            StageItemView itemView = new StageItemView();
            itemView.stage = stageData.stage;
            itemView.star = stageData.star;
            itemView.unlocked = stageData.unlocked;
            itemView.seed = stageData.seed;
            stageItemViews.Add(itemView);
        }
   
    }


    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        StageRowCell cellView = scroller.GetCellView(rowCellViewPrefab) as StageRowCell;
        // data index of the first sub cell
        var di = dataIndex * numberItemInRow;
        cellView.name = "Cell " + (di).ToString() + " to " + ((di) + numberItemInRow - 1).ToString();
        // pass in a reference to our data set with the offset for this cell
        cellView.SetData(stageItemViews, di,cellIndex);
        // return the cell to the scroller
        return cellView;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 325f;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        if(stageItemViews.Count ==0)
        {
            return 1;
        }
        return Mathf.CeilToInt((float)stageItemViews.Count / (float)numberItemInRow);
    }
    public void OnClickBack()
    {
        SceneFlowManager.Instance.LoadScene(SceneType.StartUp);
    }


}
