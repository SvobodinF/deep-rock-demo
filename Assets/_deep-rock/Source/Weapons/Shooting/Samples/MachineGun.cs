using UnityEngine;

public class MachineGun : ShootingWeapon
{
    public override void Init(ObjectPool<Bullet> objectPool, int damage, float delay)
    {
        base.Init(objectPool, damage, delay);
    }

    protected override void OnAttack()
    {
        if (HasTarget == false)
            return;

        bool canHit = CanHit(Target);

        if (canHit == false)
            return;

        Shoot();
    }
}