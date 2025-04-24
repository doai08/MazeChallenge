using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    UserData userData;

    public StageItemData currentStage;

    public void LoadStage(int _stage)
    {
        userData = DataInGameManager.Instance.GetUserData();
        var foundStage = userData.passedStages.Where(stage => stage.stage == _stage).FirstOrDefault();
        currentStage = foundStage;
        SceneFlowManager.Instance.LoadScene(SceneType.GamePlay);
        EventObserveManager.Instance.TriggerEvent(EventNameConst.LOAD_STAGE, currentStage);

    }

    public void CompleteCurrentStage(int _star)
    {
        CompleteStage(currentStage.stage, _star);
    }

    public void LoadNextStage()
    {
        var nextStage = currentStage.stage + 1;
        LoadStage(nextStage);
    }

    private void CompleteStage(int _stage, int _star)
    {
        userData = DataInGameManager.Instance.GetUserData();
        var foundStage = userData.passedStages.Where(stage => stage.stage == _stage).FirstOrDefault();
        foundStage.star = _star;
        foundStage.unlocked = true;
        DataInGameManager.Instance.SaveUserData(userData);
    }
    public void AddStage()
    {
        userData = DataInGameManager.Instance.GetUserData();
        int offSet = 999;
        int amount = userData.passedStages.Count;

        int total = amount + offSet;
        
        for (int i = amount+1; i <= total; i++)
        {
            StageItemData stageItemData = new StageItemData();
            stageItemData.stage = i;
            stageItemData.star = 0;
            //Nếu Level1 => Mở
            if (i == 1)
            {
                stageItemData.unlocked = true;
            }
            else
            {
                stageItemData.unlocked = false;
            }

            stageItemData.seed = i;
            userData.passedStages.Add(stageItemData);

        }
        DataInGameManager.Instance.SaveUserData(userData);
    }
    public void UnlockStage(int stage)
    {
        userData = DataInGameManager.Instance.GetUserData();

        if (userData.passedStages.Count == 0) return;
        for (int i = 0; i < stage; i++)
        {
            userData.passedStages[i].unlocked = true;

        }
        DataInGameManager.Instance.SaveUserData(userData);
    }
    public void RandomStar()
    {
        userData = DataInGameManager.Instance.GetUserData();
        foreach (var stage in userData.passedStages)
        {
            if (stage.unlocked)
            {
                stage.star = Random.Range(0, 4);
            }
            else
            {
                break;
            }
        }
        DataInGameManager.Instance.SaveUserData(userData);
    }
}
