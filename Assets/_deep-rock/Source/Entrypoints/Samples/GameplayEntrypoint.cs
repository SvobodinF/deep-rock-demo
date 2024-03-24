using UnityEngine;

public class GameplayEntrypoint : Entrypoint
{
    [Header("Installers")]
    [SerializeField] private EnvironmentInstaller _environmentInstaller;
    [SerializeField] private PlayerInstaller _playerInstaller;
    [SerializeField] private ViewportInstaller _viewportInstaller;
    [SerializeField] private EnemyInstaller _enemyInstaller;

    [Header("ScriptableObjects")]
    [SerializeField] private GameConfiguration _gameConfiguration;

    private Level _level;

    public override void Start()
    {
        Application.targetFrameRate = _gameConfiguration.TargetFPS;

        //Implement here levelIndex loading from data storage
        int levelIndex = 0;
        //Implement here levelIndex loading from data storage

        EnvironmentData environmentData = new EnvironmentData(_gameConfiguration.EnvironmentConfiguration, levelIndex);
        LevelConfiguration levelConfiguration = environmentData.EnvironmentConfiguration.GetLevelConfigurationbyIndex(levelIndex);

        _environmentInstaller.OnEnvironmentInstalledEvent += SetupPlayer;
        _playerInstaller.OnPlayerInstalledEvent += SetupViewport;
        _playerInstaller.OnPlayerInstalledEvent += (aliveble) => SetupEnemies(levelConfiguration, aliveble, _level);

        _environmentInstaller.Install(environmentData);
    }

    private void SetupPlayer(Level level)
    {
        _level = level;

        PlayerInstallData playerInstallData = new PlayerInstallData(_gameConfiguration.PlayerConfiguration, _level.SpawnPosition);
        _playerInstaller.Install(playerInstallData);
    }

    private void SetupViewport(IAliveble aliveble)
    {
        ViewportConfiguration configuration = new ViewportConfiguration(aliveble);
        _viewportInstaller.Install(configuration);
    }

    private void SetupEnemies(LevelConfiguration configuration, IAliveble aliveble, Level level)
    {
        EnemiesData enemiesData = new EnemiesData(configuration.EnemyConfigurations, aliveble, level);
        _enemyInstaller.Install(enemiesData);
    }
}
