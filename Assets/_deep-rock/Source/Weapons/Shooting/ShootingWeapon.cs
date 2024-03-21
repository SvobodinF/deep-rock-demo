using UnityEngine;

public abstract class ShootingWeapon : Weapon 
{
    public bool TargetAvailableToHit => CanHit(Target);

    [SerializeField] private float _radius;
    [SerializeField] protected Transform Center;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private LayerMask _blockableMask;

    private ObjectPool<Bullet> _objectPool;

    public float Angle => Utils.Utils2D.GetAngleBetween(Center, Target.transform);

    protected override Transform Target => FindNearestTarget();

    private Overlap2D _overlap2D;

    public virtual void Init(ObjectPool<Bullet> objectPool, int damage, float delay)
    {
        _objectPool = objectPool;
        _overlap2D = new CirculOverlap(Center, LayerMask, _radius);

        base.Init(damage, delay);
    }

    protected Transform FindNearestTarget()
    {
        return _overlap2D.GetNearestTarget();
    }

    protected bool CanHit(Transform target)
    {
        if (target == null)
            return false;

        Vector2 direction = target.position - Center.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _radius, _blockableMask);

        return hit.collider == null;
    }

    protected void Shoot()
    {
        Debug.Log("Shoot");
        Bullet bullet = _objectPool.Get();
        BulletInitialData bulletInitialData = new BulletInitialData(_muzzle.position, Angle, Damage);
        bullet.Init(bulletInitialData, (bullet) => _objectPool.Release(bullet));
    }

    protected override void OnGizmosDebug()
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
