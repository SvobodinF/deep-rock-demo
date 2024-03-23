using UnityEngine;

public abstract class WeaponConfiguration : Configuration
{
    public int Damage => _damage;
    public float ActionDelay => _actionDelay;

    [SerializeField] private int _damage;
    [SerializeField] private float _actionDelay;
}
