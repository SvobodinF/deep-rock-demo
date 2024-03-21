using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerConfiguration), menuName = "Configurations/Actors/PlayerConfiguration", order = 51)]
public class PlayerConfiguration : Configuration
{
    public Controller InputController => _controller;
    public MovementConfiguration MovementConfiguration => _movementConfiguration;

    [SerializeField] private Controller _controller;
    [SerializeField] private MovementConfiguration _movementConfiguration;
}
