using System;
using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    public event Action<Vector3> OnLevelChangedEvent;

    public Vector3 SpawnPosition => _spawnPoint.position;

    [SerializeField, Range(0f, 1f)] private float _difficult;
    [SerializeField] private float _minFrequency;
    [SerializeField] private float _maxFrequency;
    [SerializeField] private EnemyHandler[] _enemyHandlers;
    [SerializeField] private TileHandler _tileHandler;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private OreInstaller _oreInstaller;
 
    private float _frequency => Utils.Utils.EvaluteFloat(_minFrequency, _maxFrequency, _difficult);

    public void Init(ITransformable transformable, EnemySpawner spawner, OreLevelConfiguration[] oreLevelConfigurations)
    {
        foreach (EnemyHandler handler in _enemyHandlers)
        {
            handler.Init(transformable, spawner);
        }

        _tileHandler.OnTileRemovedEvent += (coordinates, worldPos) => OnLevelChangedEvent?.Invoke(worldPos);
        SetupOre(oreLevelConfigurations);

        _tileHandler.Init();
        _oreInstaller.Generate();

        StartCoroutine(WaitEnemySpawn());
    }

    private void SetupOre(OreLevelConfiguration[] oreLevelConfigurations)
    {
        _oreInstaller.Install(new OreInitialData(_tileHandler, oreLevelConfigurations));
    }

    private IEnumerator WaitEnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_frequency);

            foreach (EnemyHandler handler in _enemyHandlers)
            {
                if (handler.CanSpawn == false)
                    continue;

                handler.CreateNewEnemy();
            }
        }
    }
}
