using Rules;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class DebugPanelCore : IDisposable
{
    [Inject]
    private CoreController coreController;

    public DebugPanelCore()
    {

    }

    public void Init()
    {
        SROptions.Current.nextLevel += NextLevel;
    }

    private void NextLevel()
    {
        Debug.Log("[DEBUG PANEL] Go to next level");
        coreController?.GameVictory();
    }

    public void Dispose()
    {
        SROptions.Current.nextLevel -= NextLevel;
    }
}
