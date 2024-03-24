using System;
using UnityEngine;

[Serializable]
public struct LevelConfiguration
{
    public Level Level => _level;
    public EnemyConfiguration[] EnemyConfigurations => _enemyConfigurations;

    [Header("Prefabs")]
    [SerializeField] private Level _level;

    [Header("Enemies")]
    [SerializeField] private EnemyConfiguration[] _enemyConfigurations;
}

