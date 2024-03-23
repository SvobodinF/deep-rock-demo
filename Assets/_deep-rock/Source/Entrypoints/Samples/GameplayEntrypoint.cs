using UnityEngine;

public class GameplayEntrypoint : Entrypoint
{
    [Header("Installers")]
    [SerializeField] private PlayerInstaller _playerInstaller;
    [SerializeField] private ViewportInstaller _viewportInstaller;

    [Header("ScriptableObjects")]
    [SerializeField] private GameConfiguration _gameConfiguration;

    public override void Start()
    {
        Application.targetFrameRate = _gameConfiguration.TargetFPS;

        _playerInstaller.OnPlayerInstalledEvent += SetupViewport;
        _playerInstaller.Install(_gameConfiguration.PlayerConfiguration);
    }

    private void SetupViewport(IAliveble aliveble)
    {
        ViewportConfiguration configuration = new ViewportConfiguration(aliveble);

        _viewportInstaller.Install(configuration);
    }
}
