using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInGameManager : Singleton<DataInGameManager>
{
	public  UserData _userData;
	private GamePlayData _gamePlayData;

	private void InitGamePlayData()
	{
		_gamePlayData = new GamePlayData()
		{
			currentCoin = PlayDataInitConst.COIN,
			currentHeart = PlayDataInitConst.MAX_HEART,
		};
		
	}
	private void InitUserData()
	{
		_userData = new UserData()
		{
			coin = UserDataInitConst.COIN,
			isMusic = UserDataInitConst.IS_MUSIC,
			isSound = UserDataInitConst.IS_SOUND
		};
		SaveUserData(_userData);
	}
	public GamePlayData GetGamePlayData()
	{
		if(_gamePlayData == null)
		{
			InitGamePlayData();
		}
		return _gamePlayData;
	}
	public UserData GetUserData()
	{
		if (!PlayerPrefs.HasKey(GameConst.DATA_USER))
		{
			InitUserData();	
		}
		else
		{
			_userData = JsonConvert.DeserializeObject<UserData>(PlayerPrefs.GetString(GameConst.DATA_USER));
		}
		return _userData;
	}

	public void SaveUserData(UserData userData)
	{
		PlayerPrefs.SetString(GameConst.DATA_USER, JsonConvert.SerializeObject(userData));
	}
	public void SaveGamePlayData(GamePlayData gamePlayData)
	{
		_gamePlayData = gamePlayData;
	}
	public void ResetGamePlayData()
	{
		InitGamePlayData();
	}
	public void ResetUserData()
	{
		InitUserData();
	}
}
