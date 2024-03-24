using UnityEngine;

public class MachineGun : ShootingWeapon
{
    protected override void OnAttack()
    {
        if (HasTarget == false)
        {
            Debug.Log("HasTarget false");
            return;
        }

        bool canHit = CanHit(Target);

        if (canHit == false)
        {
            Debug.Log("CanHit false");
            return;
        }

        Shoot();
    }
}
