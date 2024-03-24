using System;
using UnityEngine;

public class EnvironmentInstaller : Installer<EnvironmentData>
{
    public event Action<Level> OnEnvironmentInstalledEvent;
    [SerializeField] private AstarPath _astarPath;

    public override void Install(EnvironmentData configuration)
    {
        EnvironmentConfiguration environment = configuration.EnvironmentConfiguration;
        int levelIndex = configuration.LevelIndex;

        LevelConfiguration levelConfiguration = environment.GetLevelConfigurationbyIndex(levelIndex);

        Level level = Instantiate(levelConfiguration.Level, transform);

        OnEnvironmentInstalledEvent?.Invoke(level);

        RescanNavigationGrid();

        level.OnLevelChangedEvent += RescanNavigationGrid;
    }

    private void RescanNavigationGrid()
    {
        _astarPath.Scan();
    }
}

public struct EnvironmentData : IConfiguration
{
    public readonly EnvironmentConfiguration EnvironmentConfiguration;
    public readonly int LevelIndex;

    public EnvironmentData(EnvironmentConfiguration environmentConfiguration, int levelIndex)
    {
        EnvironmentConfiguration = environmentConfiguration;
        LevelIndex = levelIndex;
    }
}
