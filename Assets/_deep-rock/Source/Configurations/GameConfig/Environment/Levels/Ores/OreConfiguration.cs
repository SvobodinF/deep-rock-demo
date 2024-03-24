using UnityEngine;

[CreateAssetMenu(fileName = nameof(OreConfiguration), menuName = "Configurations/Level/OreConfiguration", order = 51)]
public class OreConfiguration : Configuration
{
    public Ore OrePrefab => _orePrefab;

    [SerializeField] private Ore _orePrefab;
}
