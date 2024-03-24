using System;

public interface IAliveble : IDamageable, ITransformable
{
    public bool IsAlive { get; }
    public event Action<float> OnHeathPercentChangedEvent;
    public event Action OnDiedEvent;
}
