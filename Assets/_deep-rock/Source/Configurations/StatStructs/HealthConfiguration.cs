using System;
using UnityEngine;

[Serializable]
public struct HealthConfiguration
{
    public int MaxHealth => _maxHealth;
    [SerializeField] private int _maxHealth;
}
