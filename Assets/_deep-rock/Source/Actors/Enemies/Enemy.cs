using System.Collections;
using Pathfinding;
using UnityEngine;

public abstract class Enemy : Actor<EnemyInitialData>
{
    [SerializeField] private AIPath _aIPath;
    [SerializeField] private AIDestinationSetter _aIDestinationSetter;

    private Vector3 _direction => _aIPath.desiredVelocity;
    private float _velocity => Mathf.Max(Mathf.Abs(_aIPath.desiredVelocity.x), Mathf.Abs(_aIPath.desiredVelocity.y));

    protected override void OnInit(EnemyInitialData data)
    {
        EnemyConfiguration configuration = data.EnemyConfiguration;

        OnValidate();

        _aIPath.maxSpeed = configuration.MovementConfiguration.MaxSpeed;
        _aIDestinationSetter.target = data.Target.transform;
    }

    private void OnEnable()
    {
        OnDiedEvent += () => StartCoroutine(OnDied());
        OnHeathPercentChangedEvent += OnDamaged;
    }

    private void OnDisable()
    {
        OnDiedEvent -= () => StartCoroutine(OnDied());
        OnHeathPercentChangedEvent -= OnDamaged;
    }

    private void OnValidate()
    {
        _aIPath = GetComponent<AIPath>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    protected override void Rotate()
    {
        if (_direction.x == 0 && _direction.y == 0)
        {
            return;
        }
        else
        {
            transform.eulerAngles = GetRotationByVelocity();
        }
    }

    private void OnDamaged(float damage)
    {
        AnimationHandler.Set<HitAnimationParameter, int>(0);
    }

    private IEnumerator OnDied()
    {
        AnimationHandler.Set<DieAnimationParameter, int>(0);

        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);
    }

    private Vector3 GetRotationByVelocity()
    {
        if (_velocity == 0)
            return new Vector3(0f, transform.eulerAngles.y, 0f);

        float rotation = _direction.x > 0 ? 0f : 180f;
        return new Vector3(0f, rotation, 0f);
    }
}

public struct EnemyInitialData
{
    public readonly EnemyConfiguration EnemyConfiguration;
    public readonly ITransformable Target;

    public EnemyInitialData(EnemyConfiguration enemyConfiguration, ITransformable target)
    {
        EnemyConfiguration = enemyConfiguration;
        Target = target;
    }
}