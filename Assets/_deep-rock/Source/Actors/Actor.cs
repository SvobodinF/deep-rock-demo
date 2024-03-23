using UnityEngine;
using NaughtyAttributes;
using System;

public abstract class Actor<T> : MonoBehaviour, IAliveble where T : struct
{
    [SerializeField] protected Transform Model;
    [ShowNonSerializedField] protected AnimationHandler AnimationHandler;

    public event Action<float> OnHeathPercentChangedEvent;
    public event Action OnDiedEvent;

    public virtual bool IsAlive => Health > 0;

    protected int Health { get; private set; }
    protected int MaxHealth { get; private set; }
    private float _healthPercent => Health / (float)MaxHealth;

    public void Init(T configuration, IActorInitialData actorInitialData)
    {
        OnValidate();

        MaxHealth = actorInitialData.HealthConfiguration.MaxHealth;
        Health = MaxHealth;
        OnHeathPercentChangedEvent?.Invoke(_healthPercent);

        OnInit(configuration);
    }

    protected abstract void OnInit(T configuration);
    protected abstract void Rotate();
    protected abstract void OnAnimate();

    private void Update()
    {
        Rotate();
        OnAnimate();
    }

    private void OnValidate()
    {
        AnimationHandler = GetComponent<AnimationHandler>();
    }

    public void OnDamage(int damage)
    {
        if (IsAlive == false)
            return;

        Health -= damage;
        OnHeathPercentChangedEvent?.Invoke(_healthPercent);

        if (Health <= 0)
        {
            OnDiedEvent?.Invoke();
        }
    }

    [NaughtyAttributes.Button]
    private void DebugDamage()
    {
        OnDamage(10);
    }
}
