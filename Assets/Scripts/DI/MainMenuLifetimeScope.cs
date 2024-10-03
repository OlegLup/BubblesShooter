using MainMenu;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<MainMenuWindowUiController>();

        builder.RegisterEntryPoint<MainMenuInitializer>();
    }
}
