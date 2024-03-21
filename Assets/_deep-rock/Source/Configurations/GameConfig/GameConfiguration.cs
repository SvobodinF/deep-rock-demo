using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameConfiguration), menuName = "Configurations/GameConfiguration", order = 51)]
public class GameConfiguration : Configuration
{
    public PlayerConfiguration PlayerConfiguration => _playerConfiguration;

    [SerializeField] private PlayerConfiguration _playerConfiguration;
}
