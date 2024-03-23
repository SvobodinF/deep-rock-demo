using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerConfiguration), menuName = "Configurations/Actors/PlayerConfiguration", order = 51)]
public class PlayerConfiguration : Configuration, IActorInitialData
{
    public Controller InputController => _controller;
    public ShootingWeaponConfiguration ShootingWeaponConfiguration => _shootingWeaponConfiguration;
    public MeleeWeaponConfiguration MeleeWeaponConfiguration => _meleeWeaponConfiguration;
    public HealthConfiguration HealthConfiguration => _healthConfiguration;
    public MovementConfiguration MovementConfiguration => _movementConfiguration;

    [SerializeField] private Controller _controller;
    [SerializeField] private ShootingWeaponConfiguration _shootingWeaponConfiguration;
    [SerializeField] private MeleeWeaponConfiguration _meleeWeaponConfiguration;

    [Header("Stats")]
    [SerializeField] private HealthConfiguration _healthConfiguration;
    [SerializeField] private MovementConfiguration _movementConfiguration;
}
