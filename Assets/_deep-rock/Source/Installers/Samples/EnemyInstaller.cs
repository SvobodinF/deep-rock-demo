public class EnemyInstaller : Installer<EnemiesData>, ITransformable
{
    public override void Install(EnemiesData configuration)
    {
        EnemySpawner enemySpawner = new EnemySpawner(configuration.EnemyConfigurations, this);

        Level level = configuration.Level;
        level.Init(configuration.Target, enemySpawner, configuration.OreLevelConfiguration);
    }
}

public struct EnemiesData : IConfiguration
{
    public readonly OreLevelConfiguration[] OreLevelConfiguration;
    public readonly EnemyConfiguration[] EnemyConfigurations;
    public readonly ITransformable Target;
    public readonly Level Level;

    public EnemiesData(OreLevelConfiguration[] oreLevelConfiguration, EnemyConfiguration[] enemyConfigurations, ITransformable target, Level level)
    {
        OreLevelConfiguration = oreLevelConfiguration;
        EnemyConfigurations = enemyConfigurations;
        Target = target;
        Level = level;
    }
}