using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class OneDirectionBullet : Bullet
{
    [SerializeField] private float _dispersion;

    public override void Init(BulletInitialData bulletInitialData, Action<Bullet> onDisable)
    {
        float angle = bulletInitialData.Angle + Random.Range(-_dispersion, _dispersion);

        base.Init(bulletInitialData, onDisable);
        Rigidbody2D.velocity = Utils.Utils2D.DegreeToVector2(angle) * Speed * Time.fixedDeltaTime;   
    }

    protected override void OnCollision(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnDamage(Damage);
        }

        StopAllCoroutines();
        Disable();
    }

    protected override void Update()
    {
        var direction = Rigidbody2D.velocity;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
