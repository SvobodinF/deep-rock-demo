using System;
using UnityEngine;

[Serializable]
public struct MovementConfiguration
{
    public float MaxSpeed => _maxSpeed;

    [SerializeField] private float _maxSpeed;
}
