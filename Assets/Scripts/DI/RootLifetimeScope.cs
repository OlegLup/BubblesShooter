using UnityEngine;
using VContainer;
using VContainer.Unity;
using Data;
using MoreMountains.Tools;

public class RootLifetimeScope : LifetimeScope
{
    [SerializeField]
    private RootSettings rootSettings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<GameSceneLoader>();
        builder.RegisterInstance(rootSettings);
        builder.Register<GameData>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<GameDataController>();
        builder.RegisterComponentInHierarchy<MMSoundManager>();

        builder.RegisterComponentInHierarchy<SettingsWindowUiController>();

        builder.RegisterEntryPoint<RootInitializer>();
    }
}
