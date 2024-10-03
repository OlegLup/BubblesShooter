using UnityEngine;
using VContainer;
using VContainer.Unity;
using Data;

public class RootInitializer : IStartable
{
    [Inject]
    private GameDataController gameDataController;

    void IStartable.Start()
    {
        gameDataController.Init();
    }
}
