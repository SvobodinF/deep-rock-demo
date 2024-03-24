public class EnemyInstaller : Installer<EnemiesData>, ITransformable
{
    public override void Install(EnemiesData configuration)
    {
        EnemySpawner enemySpawner = new EnemySpawner(configuration.EnemyConfigurations, this);

        Level level = configuration.Level;
        level.Init(configuration.Target, enemySpawner);
    }
}

public struct EnemiesData : IConfiguration
{
    public readonly EnemyConfiguration[] EnemyConfigurations;
    public readonly ITransformable Target;
    public readonly Level Level;

    public EnemiesData(EnemyConfiguration[] enemyConfigurations, ITransformable player, Level level)
    {
        EnemyConfigurations = enemyConfigurations;
        Target = player;
        Level = level;
    }
}