using UnityEngine;

[CreateAssetMenu(fileName = nameof(MovementConfiguration), menuName = "Configurations/Actors/Movement/MovementConfiguration", order = 51)]
public class MovementConfiguration : Configuration
{
    public float MaxSpeed => _maxSpeed;

    [SerializeField] private float _maxSpeed;
}
