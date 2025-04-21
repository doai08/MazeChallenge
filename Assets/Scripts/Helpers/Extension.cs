using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension 
{
	public static void ChangeLayer(this GameObject obj, int layer)
	{
		foreach (Transform trans in obj.GetComponentsInChildren<Transform>(true))
			trans.gameObject.layer = layer;
	}
}
