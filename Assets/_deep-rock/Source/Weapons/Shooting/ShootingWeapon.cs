using UnityEngine;

public abstract class ShootingWeapon : Weapon<IDamageable> 
{
    public bool TargetAvailableToHit => CanHit(Target);

    [SerializeField] private float _radius;
    [SerializeField] protected Transform Center;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private LayerMask _blockableMask;

    private ObjectPool<Bullet> _objectPool;

    public float Angle => Utils.Utils2D.GetAngleBetween(Center, Target.transform);

    protected override IDamageable Target => FindNearestTarget();

    private Overlap2D _overlap2D;

    public virtual void Init(ObjectPool<Bullet> objectPool, int damage, float delay)
    {
        _objectPool = objectPool;
        _overlap2D = new CirculOverlap(Center, LayerMask, _radius);

        base.Init(damage, delay);
    }

    protected IDamageable FindNearestTarget()
    {
        if (_overlap2D == null)
            return null;

        return _overlap2D.GetNearestTarget<IDamageable>();
    }

    protected bool CanHit(ITransformable target)
    {
        if (target == null)
        {
            Debug.Log("Target is null");
            return false;
        }

        Vector2 direction = target.transform.position - Center.position;
        float distance = Vector2.Distance(target.transform.position, Center.position);
        RaycastHit2D hit = Physics2D.Raycast(Center.position, direction, distance, _blockableMask);
        Debug.DrawRay(Center.position, direction, Color.red, 0.1f);

        Debug.Log($"HitCollider is {(hit.collider == null ? "null" : hit.collider.name)}");

        return hit.collider == null;
    }

    protected void Shoot()
    {
        Bullet bullet = _objectPool.Get();
        BulletInitialData bulletInitialData = new BulletInitialData(_muzzle.position, Angle, Damage);
        bullet.Init(bulletInitialData, (bullet) => _objectPool.Release(bullet));
    }

    protected override void OnDrawGizmos()
    {
        if (_overlap2D == null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Center.position, _radius);
            return;
        }
        
        _overlap2D.DrawOverlap(Color.green);
    }
}
