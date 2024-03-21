using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D Rigidbody2D;

    [Header("Bullet Settings")]
    [SerializeField] protected float Speed;
    [SerializeField] private float _lifeTime;

    private protected int Damage;
    private event Action<Bullet> OnDisableEvent;

    public virtual void Init(BulletInitialData bulletInitialData, Action<Bullet> onDisable)
    {
        Damage = bulletInitialData.Damage;
        OnDisableEvent = onDisable;
        transform.SetPositionAndRotation(bulletInitialData.Position,
            Quaternion.Euler(0f, 0f, bulletInitialData.Angle));

        StartCoroutine(WaitDisable(_lifeTime));
    }

    protected abstract void Update();
    protected void Disable()
    {
        OnDisableEvent?.Invoke(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision);
    }

    protected abstract void OnCollision(Collision2D collision);

    private IEnumerator WaitDisable(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        Disable();
    }
}

public struct BulletInitialData
{
    public readonly Vector3 Position;
    public readonly float Angle;
    public readonly int Damage;

    public BulletInitialData(Vector3 position, float angle, int damage)
    {
        Position = position;
        Angle = angle;
        Damage = damage;
    }
}