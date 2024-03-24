using System;
using UnityEngine;

[Serializable]
public class LevelConfiguration
{
    public Level Level => _level;
    public EnemyConfiguration[] EnemyConfigurations => _enemyConfigurations;
    public OreLevelConfiguration[] OreConfigurations => _oreConfigurations;

    [Header("Prefabs")]
    [SerializeField] private Level _level;

    [Header("Enemies")]
    [SerializeField] private EnemyConfiguration[] _enemyConfigurations;

    [Header("Ores")]
    [SerializeField] private OreLevelConfiguration[] _oreConfigurations;
}

[Serializable]
public class OreLevelConfiguration : IConfiguration
{
    public OreConfiguration OreConfiguration => _oreConfiguration;
    public int Count;

    [SerializeField] private OreConfiguration _oreConfiguration;

    public OreLevelConfiguration(OreLevelConfiguration configuration)
    {
        Count = configuration.Count;
        _oreConfiguration = configuration.OreConfiguration;
    }
}