using System.Collections;
using UnityEngine;

public abstract class Enemy : Actor<EnemyData>
{
    [SerializeField] private Movement _movement;

    protected override void OnInit(EnemyData data)
    {
        EnemyConfiguration configuration = data.EnemyConfiguration;

        OnValidate();

        _movement.Init(data.Controller, this, configuration.MovementConfiguration);
        AnimationHandler.Init();

        //_shootingWeapon.Init(data.BulletPool, weaponConfiguration.Damage, weaponConfiguration.ActionDelay);
        //_digWeapon.Init(data.Controller, configuration.MeleeWeaponConfiguration);
    }

    private void OnEnable()
    {
        OnDiedEvent += () => StartCoroutine(OnDied());
    }

    private void OnDisable()
    {
        OnDiedEvent -= () => StartCoroutine(OnDied());
    }

    private void OnValidate()
    {
        _movement = GetComponent<Movement>();
    }

    protected override void Rotate()
    {
        if (_movement.Direction.x == 0 && _movement.Direction.y == 0)
        {
            return;
        }
        else
        {
            transform.eulerAngles = _movement.GetRotationByVelocity();
        }
    }

    protected override void OnAnimate()
    {
        AnimationHandler.Set<SpeedAnimationParameter, float>(_movement.Velocity);
    }

    private IEnumerator OnDied()
    {
        AnimationHandler.Set<DieAnimationParameter, int>(0);

        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);
    }
}


public struct EnemyData
{
    public readonly EnemyConfiguration EnemyConfiguration;
    public readonly IController Controller;
    public readonly ObjectPool<Bullet> BulletPool;

    public EnemyData(EnemyConfiguration enemyConfiguration, IController controller, ObjectPool<Bullet> bulletPool)
    {
        EnemyConfiguration = enemyConfiguration;
        Controller = controller;
        BulletPool = bulletPool;
    }
}