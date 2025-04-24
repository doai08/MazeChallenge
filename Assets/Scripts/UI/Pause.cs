using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : UIScreen
{
    [SerializeField] private Button btnContinue, btnHome, btnSound;
    public void OnClickContinue()
    {
        UIManager.Instance.HidePopup();
    }
       public void OnClickHome()
    {
         UIManager.Instance.HidePopup();
        SceneFlowManager.Instance.LoadScene(SceneType.StartUp);
    }
    public void OnClickSound()
    {

    }
}
