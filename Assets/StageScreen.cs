using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class StageScreen : UIScreen, IEnhancedScrollerDelegate
{

    [SerializeField] EnhancedScroller scroller;
    private UserData userData;


    [SerializeField] private EnhancedScrollerCellView rowCellViewPrefab;

    private int numberItemInRow = 4;




    void Start()
    {
 
        Application.targetFrameRate = 60;
        scroller.Delegate = this;
       
    }
    void LoadData()
    {

    }


    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        throw new System.NotImplementedException();
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        throw new System.NotImplementedException();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update

}
