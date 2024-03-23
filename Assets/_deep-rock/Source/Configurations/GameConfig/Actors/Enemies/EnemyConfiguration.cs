using UnityEngine;

public abstract class EnemyConfiguration : Configuration, IActorInitialData
{
    public HealthConfiguration HealthConfiguration => _healthConfiguration;
    public MovementConfiguration MovementConfiguration => _movementConfiguration;

    [Header("Stats")]
    [SerializeField] private HealthConfiguration _healthConfiguration;
    [SerializeField] private MovementConfiguration _movementConfiguration;
}
