using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuedScreen
{
	public UIScreen screen;
	public ScreenInfo info;
	public bool popIt;
	public string screenName;

	public QueuedScreen(UIScreen screen, ScreenInfo info, bool popIt)
	{
		this.screen = screen;
		this.info = info;
		this.popIt = popIt;
	}
}
