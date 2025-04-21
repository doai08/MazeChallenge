using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SimpleCoroutine : Singleton<SimpleCoroutine>
{
	public void Delay(float delaySeconds, Action callback)
	{
		StartCoroutine(DelayCoroutine(delaySeconds, callback));
	}

	private IEnumerator DelayCoroutine(float delaySeconds, Action callback)
	{
		yield return new WaitForSeconds(delaySeconds); 
		callback?.Invoke(); 
	}
}
