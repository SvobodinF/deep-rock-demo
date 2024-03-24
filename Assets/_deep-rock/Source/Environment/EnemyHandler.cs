using UnityEngine;

public class EnemyHandler : MonoBehaviour, ITransformable
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    [Header("Debug")]
    [SerializeField] private Color _color;

    private Overlap2D _overlap2D;

    private ITransformable _player;
    private EnemySpawner _enemySpawner;

    public bool CanSpawn => _aliveble == null;
    private IAliveble _aliveble;

    public void Init(ITransformable player, EnemySpawner spawner)
    {
        _player = player;
        _enemySpawner = spawner;
    }

    private void OnEnable()
    {
        _overlap2D = new CirculOverlap(transform, _layerMask, _radius);
    }

    private void Update()
    {       
        _aliveble = _overlap2D.GetTarget<IAliveble>();
    }

    public void CreateNewEnemy()
    {
        Enemy enemy = _enemySpawner.GetEnemy(out EnemyConfiguration configuration);

        EnemyInitialData enemyInitialData = new EnemyInitialData(configuration, _player);
        enemy.Init(enemyInitialData, configuration);
        enemy.transform.position = GetPosition(this);
    }

    private Vector3 GetPosition(ITransformable target)
    {
        Vector3? result = null;

        while (result == null)
        {
            Vector3Int targetInt = new Vector3Int((int)target.transform.position.x, (int)target.transform.position.y, 0);
            Vector3Int worldPosition = targetInt;
            result = new Vector3(worldPosition.x, worldPosition.y, 0f);
        }

        return result.Value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
