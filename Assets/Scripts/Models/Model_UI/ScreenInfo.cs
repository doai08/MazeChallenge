using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInfo
{
	private Dictionary<string, object> dict;

	public ScreenInfo()
	{
		this.dict = new Dictionary<string, object>();
	}

	public void Add(string key, object obj)
	{
		if (!this.dict.ContainsKey(key))
		{
			this.dict.Add(key, obj);
		}
		else
		{
			throw new System.Exception("ScreenInfo already contains an entry with the key: " + key);
		}
	}

	public void Replace(string key, object obj)
	{
		if (this.dict.ContainsKey(key))
		{
			this.dict[key] = obj;
		}
		else
		{
			throw new System.Exception("ScreenInfo does not contain an entry with the key: " + key);
		}
	}

	public bool KeyExists(string key)
	{
		return this.dict.ContainsKey(key);
	}

	public T TryGet<T>(string key)
	{
		return this.Get<T>(key, false);
	}

	public T Get<T>(string key)
	{
		return this.Get<T>(key, true);
	}

	private T Get<T>(string key, bool assert)
	{
		if (this.dict.ContainsKey(key))
			return (T)this.dict[key];
		else
		{
			if (assert)
				throw new System.Exception("ScreenInfo does not contain an entry for key: " + key);

			return default(T);
		}
	}
}
