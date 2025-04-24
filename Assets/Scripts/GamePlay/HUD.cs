using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtUnlockStage;
    int unlockStage;
    UserData userData;


    void Start()
    {
        SetRandomUnlockStage();
    }

    void SetRandomUnlockStage()
    {
        userData = DataInGameManager.Instance.GetUserData();
        var maxStage = userData.passedStages.Count;
        unlockStage = Random.Range(1, maxStage);
        txtUnlockStage.text = $"UNLOCK STAGE < {unlockStage}";
    }

    public void AddStage()
    {
        StageManager.Instance.AddStage();
    }
    public void OnClickPlay()
    {
        AudioManager.Instance.PlaySoundEffect(SoundType.Click);
        SceneFlowManager.Instance.LoadScene(SceneType.Stage);
    }
    public void UnlockStage()
    {
        AudioManager.Instance.PlaySoundEffect(SoundType.Click);
        StageManager.Instance.UnlockStage(unlockStage);
        SetRandomUnlockStage();
    }
    public void ResetStage()
    {
        AudioManager.Instance.PlaySoundEffect(SoundType.Click);
        userData = DataInGameManager.Instance.GetUserData();
        userData.passedStages.Clear();
        DataInGameManager.Instance.SaveUserData(userData);
    }
    public void RandomStar()
    {
        AudioManager.Instance.PlaySoundEffect(SoundType.Click);
        StageManager.Instance.RandomStar();
    }
}
