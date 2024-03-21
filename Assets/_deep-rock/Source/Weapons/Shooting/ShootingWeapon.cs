using UnityEngine;

public abstract class ShootingWeapon : Weapon 
{
    [SerializeField] private float _radius;
    [SerializeField] protected Transform Center;
    [SerializeField] private Transform _muzzle;

    protected ObjectPool<Bullet> ObjectPool { private set; get; }
    private Overlap2D _overlap2D;

    public virtual void Init(ObjectPool<Bullet> objectPool, int damage, float delay)
    {
        ObjectPool = objectPool;

        _overlap2D = new CirculOverlap(Center, LayerMask, _radius);

        base.Init(damage, delay);
    }

    protected override void OnGizmosDebug()
    {
        _overlap2D.DrawOverlap(Color.green);
    }
}
