using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource soundSource;

	private UserData _userData;

	public AudioClip bgMusic;
	public AudioClip FX_Click;
	public AudioClip FX_Win;
	public AudioClip FX_Lose;
	public AudioClip[] FX_Combos;

	private void Start()
	{
		UpdateAudioStatus(AudioType.Music);
		UpdateAudioStatus(AudioType.Sound);

	}
	public bool GetSoundStatus()
	{
		_userData = DataInGameManager.Instance.GetUserData();
		return _userData.isSound;
	}

	public void UpdateAudioStatus(AudioType audioType)
	{
		_userData = DataInGameManager.Instance.GetUserData();
		switch (audioType)
		{
			case (AudioType.Music):
				if (_userData.isMusic)
				{
					musicSource.clip = bgMusic;
					musicSource.Play();
				}
				else
				{
					musicSource.Stop();
				}

				break;
			case (AudioType.Sound):

				if (_userData.isSound)
				{
					soundSource.Play();
				}
				else
				{
					soundSource.Stop();
				}
				break;
		}
	}
	public void MusicVolume(float volume)
	{
		musicSource.volume = volume;
	}
	
	public void PlaySoundEffect(SoundType soundType, int arrayValue = 0, float volume = 1)
	{
		_userData = DataInGameManager.Instance.GetUserData();
		if (_userData.isSound)
		{
			switch (soundType)
			{
				case (SoundType.Click):
					soundSource.PlayOneShot(FX_Click);
					break;
				case (SoundType.Win):
					soundSource.PlayOneShot(FX_Win);
					break;
				case (SoundType.Lose):
					soundSource.PlayOneShot(FX_Lose);
					break;
				case (SoundType.Combo):
					soundSource.PlayOneShot(FX_Combos[arrayValue]);
					break;
				
			}
			soundSource.volume = volume;
		}
	}
}


