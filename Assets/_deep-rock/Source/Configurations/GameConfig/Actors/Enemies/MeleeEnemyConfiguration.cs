using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MeleeEnemyConfiguration), menuName = "Configurations/Enemies/MeleeEnemyConfiguration", order = 51)]
public class MeleeEnemyConfiguration : EnemyConfiguration
{
    public override Enemy EnemyPrefab => _meleeEnemyPrefab;

    [SerializeField] private MeleeEnemy _meleeEnemyPrefab;
}
