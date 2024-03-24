using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private MeleeWeapon<IAliveble> _meleeWeapon;

    protected override void OnInit(EnemyInitialData data)
    {
        MeleeWeaponConfiguration configuration = data.EnemyConfiguration.WeaponConfiguration;
        _meleeWeapon.Init(configuration.Damage, configuration.ActionDelay, configuration.Distance);

        base.OnInit(data);
    }
}
