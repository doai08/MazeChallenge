
using UnityEngine.SceneManagement;
public class SceneFlowManager : Singleton<SceneFlowManager>
{
	public void LoadScene(SceneType sceneType)
	{
		SceneManager.LoadScene(sceneType.ToString());
	}
	public SceneType GetCurrentScene()
	{
		var currentScene = SceneManager.GetActiveScene().name;
		return (SceneType)System.Enum.Parse(typeof(SceneType), currentScene);
	}

}
