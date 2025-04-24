
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;


public class StageRowCell : EnhancedScrollerCellView
{
    [SerializeField] private GameObject BtnAddStage;
    [SerializeField] private List<StageCell> stageCells;
    [SerializeField] private Transform gridContainer;


    public void SetData(List<StageItemView> stageItemDatas, int startingIndex, int cellIndex)
    {
        Debug.Log(cellIndex + " startIndex: " + startingIndex);

        CheckShowInstruction(stageItemDatas);

        bool isEvenRow = (cellIndex % 2 == 0);

        for (int i = 0; i < stageCells.Count; i++)
        {
            int visualIndex = isEvenRow ? i : (stageCells.Count - 1 - i); // đảo hướng hiển thị
            int dataIndex = startingIndex + i; // vẫn giữ thứ tự data tăng dần

            if (dataIndex < stageItemDatas.Count)
            {
                stageCells[visualIndex].gameObject.SetActive(true);

                bool isFirstItem = (i == 0);
                bool isEndItem = (i == stageCells.Count - 1);

                stageCells[visualIndex].SetData(
                    dataIndex,
                    stageItemDatas[dataIndex],
                    isFirstItem,
                    isEndItem,
                    isEvenRow
                );
            }
            else
            {
                stageCells[visualIndex].gameObject.SetActive(false);
            }
        }
    }

    private void CheckShowInstruction(List<StageItemView> stageItemDatas)
    {
        if (stageItemDatas.Count == 0)
        {
            BtnAddStage.gameObject.SetActive(true);
        }
        else
        {
            BtnAddStage.gameObject.SetActive(false);
        }
    }
    public void ReverseChildren(Transform parent)
    {
        int count = parent.childCount;
        for (int i = 0; i < count / 2; i++)
        {
            Transform a = parent.GetChild(i);
            Transform b = parent.GetChild(count - 1 - i);

            int indexA = a.GetSiblingIndex();
            int indexB = b.GetSiblingIndex();

            a.SetSiblingIndex(indexB);
            b.SetSiblingIndex(indexA);
        }
    }
}
