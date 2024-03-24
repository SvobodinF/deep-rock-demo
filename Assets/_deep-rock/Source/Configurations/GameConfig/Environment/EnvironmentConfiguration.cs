using UnityEngine;

[CreateAssetMenu(fileName = nameof(EnvironmentConfiguration), menuName = "Configurations/EnvironmentConfiguration", order = 51)]
public class EnvironmentConfiguration : Configuration
{
    [SerializeField] private LevelConfiguration[] _levelConfigurations;

    public LevelConfiguration GetLevelConfigurationbyIndex(int index)
    {
        return Utils.Utils.GetElementOfArray(index, _levelConfigurations);
    }
}
