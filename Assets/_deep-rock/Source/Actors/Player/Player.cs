using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
[RequireComponent(typeof(PlayerAnimationHandler))]
public class Player : Actor<PlayerData>, ICameraTarget
{
    //set here health checker
    public bool IsAlive => true;

    [ShowNonSerializedField] private Movement _movement;
    [SerializeField] private ShootingWeapon _shootingWeapon;

    protected override void OnInit(PlayerData data)
    {
        OnValidate();

        _movement.Init(data.Controller, data.MovementConfiguration);
        AnimationHandler.Init();

        ShootingWeaponConfiguration configuration = data.ShootingWeaponConfiguration;
        ObjectPool<Bullet> objectPool = new ObjectPool<Bullet>(configuration.BulletPrefab, null, 100);
        _shootingWeapon.Init(objectPool, configuration.Damage, configuration.ActionDelay);
    }

    private void OnValidate()
    {
        _movement = GetComponent<Movement>();
    }

    protected override void Rotate()
    {
        if (_shootingWeapon.TargetAvailableToHit == false)
        {
            /*if (isDie == true)
            {
                transform.localScale = new Vector3(_localScaleValue, _localScaleValue, _localScaleValue);
                return;
            }*/
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
}

public struct PlayerData
{
    public readonly IController Controller;
    public readonly MovementConfiguration MovementConfiguration;
    public readonly ShootingWeaponConfiguration ShootingWeaponConfiguration;

    public PlayerData(IController controller, MovementConfiguration movementConfiguration,
        ShootingWeaponConfiguration shootingWeaponConfiguration)
    {
        Controller = controller;
        MovementConfiguration = movementConfiguration;
        ShootingWeaponConfiguration = shootingWeaponConfiguration;
    }
}