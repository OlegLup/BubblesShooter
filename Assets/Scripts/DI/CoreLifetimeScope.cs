using GameField;
using Player;
using Rules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CoreLifetimeScope : LifetimeScope
{
    [SerializeField]
    private CoreSettings settings;
    [SerializeField]
    private CoreSceneContent sceneContent;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(settings);
        builder.RegisterComponent(sceneContent);
        builder.Register<NodeField>(Lifetime.Singleton);
        builder.Register<ItemField>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<IPlayerControl>();
        builder.RegisterComponentInHierarchy<PlayerShotTrajectory>();
        builder.Register<PlayerItemQueue>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<PlayerItem>();
        builder.Register<CoreController>(Lifetime.Singleton);
        builder.Register<DebugPanelCore>(Lifetime.Singleton);

        builder.RegisterComponentInHierarchy<CoreGameplayWindowUiController>();
        builder.RegisterComponentInHierarchy<PlayerItemQueueUiController>();
        builder.RegisterComponentInHierarchy<ScoreUiController>();

        builder.RegisterEntryPoint<CoreInitializer>();
    }
}
