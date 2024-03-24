using UnityEngine;

public class ViewportInstaller : Installer<ViewportConfiguration>
{
    [SerializeField] private InGameWindow _inGameWindow;

    public override void Install(ViewportConfiguration configuration)
    {
        _inGameWindow.Init(configuration.Player, configuration.OreHandler);   
    }
}