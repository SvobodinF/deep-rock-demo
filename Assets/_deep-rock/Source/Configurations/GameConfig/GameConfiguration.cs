using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameConfiguration), menuName = "Configurations/GameConfiguration", order = 51)]
public class GameConfiguration : Configuration
{
    public int TargetFPS => _targetFPS;
    public PlayerConfiguration PlayerConfiguration => _playerConfiguration;
    public EnvironmentConfiguration EnvironmentConfiguration => _environmentConfiguration;

    [SerializeField] private int _targetFPS;
    [SerializeField] private PlayerConfiguration _playerConfiguration;
    [SerializeField] private EnvironmentConfiguration _environmentConfiguration;
}
