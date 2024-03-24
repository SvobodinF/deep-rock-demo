using UnityEngine;

public class GameplayEntrypoint : Entrypoint
{
    [Header("FSM")]
    [SerializeField] private GameStateHandler _gameStateHandler;

    [Header("Installers")]
    [SerializeField] private EnvironmentInstaller _environmentInstaller;
    [SerializeField] private PlayerInstaller _playerInstaller;
    [SerializeField] private ViewportInstaller _viewportInstaller;
    [SerializeField] private EnemyInstaller _enemyInstaller;

    [Header("ScriptableObjects")]
    [SerializeField] private GameConfiguration _gameConfiguration;

    private Level _level;
    private OreHandler _oreHandler;

    public override void Start()
    {
        Application.targetFrameRate = _gameConfiguration.TargetFPS;

        //Implement here levelIndex loading from data storage
        int levelIndex = 0;
        //Implement here levelIndex loading from data storage

        EnvironmentData environmentData = new EnvironmentData(_gameConfiguration.EnvironmentConfiguration, levelIndex);
        LevelConfiguration levelConfiguration = environmentData.EnvironmentConfiguration.GetLevelConfigurationbyIndex(levelIndex);

        _environmentInstaller.OnEnvironmentInstalledEvent += SetupPlayer;
        _playerInstaller.OnPlayerInstalledEvent += SetupDomain;
        _playerInstaller.OnPlayerInstalledEvent += SetupViewport;
        _playerInstaller.OnPlayerInstalledEvent += (aliveble) => SetupEnvironment(levelConfiguration, aliveble, _level);
        _playerInstaller.OnPlayerInstalledEvent += SetupFSM;

        _environmentInstaller.Install(environmentData);
    }

    private void SetupPlayer(Level level)
    {
        _level = level;

        PlayerInstallData playerInstallData = new PlayerInstallData(_gameConfiguration.PlayerConfiguration, _level.SpawnPosition);
        _playerInstaller.Install(playerInstallData);
    }

    private void SetupDomain(IPlayer player)
    {
        _oreHandler = new OreHandler(player.Collector);
    }

    private void SetupViewport(IPlayer player)
    {
        ViewportConfiguration configuration = new ViewportConfiguration(player, _oreHandler);
        _viewportInstaller.Install(configuration);
    }

    private void SetupEnvironment(LevelConfiguration configuration, IAliveble aliveble, Level level)
    {
        EnemiesData enemiesData = new EnemiesData(configuration.OreConfigurations, configuration.EnemyConfigurations, aliveble, level);
        _enemyInstaller.Install(enemiesData);
    }

    private void SetupFSM(IPlayer player)
    {
        _gameStateHandler.Init(player);
    }
}
