using UnityEngine;

public abstract class EnemyConfiguration : Configuration, IActorInitialData
{
    public abstract Enemy EnemyPrefab { get; }
    public HealthConfiguration HealthConfiguration => _healthConfiguration;
    public MovementConfiguration MovementConfiguration => _movementConfiguration;
    public MeleeWeaponConfiguration WeaponConfiguration => _meleeEnemyConfiguration;

    [Header("Stats")]
    [SerializeField] private HealthConfiguration _healthConfiguration;
    [SerializeField] private MovementConfiguration _movementConfiguration;
    [SerializeField] private MeleeWeaponConfiguration _meleeEnemyConfiguration;
}
