using UnityEngine;

public class EnemySpawner
{
    private EnemyConfiguration[] _configurations;
    private ObjectPool<Enemy>[] _enemyPools;

    public EnemySpawner(EnemyConfiguration[] enemyConfigurations, ITransformable transformable)
    {
        _configurations = enemyConfigurations;
        _enemyPools = new ObjectPool<Enemy>[enemyConfigurations.Length];

        for (int i = 0; i < _configurations.Length; i++)
        {
            EnemyConfiguration configuration = _configurations[i];
            _enemyPools[i] = new ObjectPool<Enemy>(configuration.EnemyPrefab, transformable.transform, 10);
        }
    }

    public Enemy GetEnemy(out EnemyConfiguration configuration)
    {
        int random = Random.Range(0, _enemyPools.Length);

        configuration = _configurations[random];
        return _enemyPools[random].Get();
    }
}
