using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public partial class SROptions
{
	public event Action nextLevel;

	[Category("Root")]
	public void DeleteSavedData()
	{
		Debug.Log("[DEBUG PANEL] Clearing PlayerPrefs");
		PlayerPrefs.DeleteAll();
	}

	[Category("Core")]
	public void NextLevel()
	{
		nextLevel?.Invoke();
	}
}
