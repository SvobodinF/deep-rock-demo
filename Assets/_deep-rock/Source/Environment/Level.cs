using System;
using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    public event Action OnLevelChangedEvent;

    public Vector3 SpawnPosition => _spawnPoint.position;

    [SerializeField, Range(0f, 1f)] private float _difficult;
    [SerializeField] private float _minFrequency;
    [SerializeField] private float _maxFrequency;
    [SerializeField] private EnemyHandler[] _enemyHandlers;
    [SerializeField] private TileHandler _tileHandler;
    [SerializeField] private Transform _spawnPoint;

    private float _frequency => Utils.Utils.EvaluteFloat(_minFrequency, _maxFrequency, _difficult);

    public void Init(ITransformable transformable, EnemySpawner spawner)
    {
        foreach (EnemyHandler handler in _enemyHandlers)
        {
            handler.Init(transformable, spawner);
        }

        _tileHandler.OnTileRemovedEvent += () => OnLevelChangedEvent?.Invoke();

        StartCoroutine(WaitEnemySpawn());
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
