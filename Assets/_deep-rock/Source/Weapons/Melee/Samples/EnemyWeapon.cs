public class EnemyWeapon : MeleeWeapon<IAliveble>
{
    protected override IAliveble Target => GetTarget();

    protected override void OnAttack()
    {
        if (HasTarget == false)
            return;

        Target.OnDamage(Damage);
    }

    private IAliveble GetTarget()
    {
        return Overlap2D.GetTarget<IAliveble>();
    }
}
