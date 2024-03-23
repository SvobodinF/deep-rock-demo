using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
[RequireComponent(typeof(PlayerAnimationHandler))]
public class Player : Actor<PlayerData>, ICameraTarget
{
    [ShowNonSerializedField] private Movement _movement;
    [SerializeField] private ShootingWeapon _shootingWeapon;
    [SerializeField] private DigWeapon _digWeapon;

    protected override void OnInit(PlayerData data)
    {
        PlayerConfiguration configuration = data.PlayerConfiguration;
        ShootingWeaponConfiguration weaponConfiguration = configuration.ShootingWeaponConfiguration;

        OnValidate();

        _movement.Init(data.Controller, this, configuration.MovementConfiguration);
        AnimationHandler.Init();

        _shootingWeapon.Init(data.BulletPool, weaponConfiguration.Damage, weaponConfiguration.ActionDelay);
        _digWeapon.Init(data.Controller, configuration.MeleeWeaponConfiguration);
    }

    private void OnEnable()
    {
        OnDiedEvent += OnDied;
    }

    private void OnDisable()
    {
        OnDiedEvent -= OnDied;
    }

    private void OnValidate()
    {
        _movement = GetComponent<Movement>();
    }

    protected override void Rotate()
    {
        if (_shootingWeapon.TargetAvailableToHit == false)
        {
            RotateShootingWeapon(transform.eulerAngles);

            if (_movement.Direction.x == 0 && _movement.Direction.y == 0)
            {
                return;
            }
            else
            {
                transform.eulerAngles = _movement.GetRotationByVelocity();
            }

            return;
        }

        bool isTargetLocatedRight = _shootingWeapon.TargetPosition.x >= transform.position.x;
        float angle = isTargetLocatedRight ? 0f : 180f;
        transform.eulerAngles = new Vector3(0f, angle, 0f);
        RotateShootingWeapon(new Vector3(0f, 0f, _shootingWeapon.Angle), isTargetLocatedRight);
    }

    private void RotateShootingWeapon(Vector3 angle, bool isTargetLocatedRight = true)
    {
        _shootingWeapon.transform.localScale = isTargetLocatedRight ? Vector3.one : new Vector3(1f, -1f, 1f);
        _shootingWeapon.transform.eulerAngles = angle;
    }

    protected override void OnAnimate()
    {
        AnimationHandler.Set<SpeedAnimationParameter, float>(_movement.Velocity);
    }

    private void OnDied()
    {
        AnimationHandler.Set<DieAnimationParameter, int>(0);
        _shootingWeapon.gameObject.SetActive(false);
        _digWeapon.gameObject.SetActive(false);
    }
}

public struct PlayerData
{
    public readonly PlayerConfiguration PlayerConfiguration;
    public readonly IController Controller;
    public readonly ObjectPool<Bullet> BulletPool;

    public PlayerData(PlayerConfiguration playerConfiguration, IController controller, ObjectPool<Bullet> bulletPool)
    {
        PlayerConfiguration = playerConfiguration;
        Controller = controller;
        BulletPool = bulletPool;
    }
}