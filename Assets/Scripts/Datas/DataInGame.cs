using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
	public int coin { get; set; }
	public bool isSound { get; set; }
	public bool isMusic { get; set; }

	public List<StageItemData> passedStages {get;set;}
}
public class GamePlayData
{
	public int currentHeart { get; set; }
	public int currentCoin { get; set; }
}
