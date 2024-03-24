using UnityEngine;

[CreateAssetMenu(fileName = nameof(MeleeWeaponConfiguration), menuName = "Configurations/Weapon/MeleeWeaponConfiguration", order = 51)]
public class MeleeWeaponConfiguration : WeaponConfiguration
{
    public float Distance => _distance;

    [SerializeField] private float _distance;
}
