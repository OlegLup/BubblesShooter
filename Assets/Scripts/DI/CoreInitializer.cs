using GameField;
using Player;
using Rules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CoreInitializer : IStartable
{
    [Inject]
    private ItemField itemField;
    [Inject]
    private IPlayerControl playerControl;
    [Inject]
    private PlayerItemQueue playerItemQueue;
    [Inject]
    private PlayerShotTrajectory playerShotTrajectory;
    [Inject]
    private PlayerItem playerItem;
    [Inject]
    private CoreController coreRules;
    [Inject]
    private DebugPanelCore debugPanelCore;


    void IStartable.Start()
    {
        itemField.Init();
        playerControl.Init();
        playerItemQueue.Init();
        playerShotTrajectory.Init();
        playerItem.Init();
        coreRules.Init();
        debugPanelCore.Init();
    }
}
