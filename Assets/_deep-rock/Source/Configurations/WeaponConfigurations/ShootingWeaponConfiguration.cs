using UnityEngine;

[CreateAssetMenu(fileName = nameof(ShootingWeaponConfiguration), menuName = "Configurations/Weapon/ShootingWeaponConfiguration", order = 51)]
public class ShootingWeaponConfiguration : WeaponConfiguration
{
    public Bullet BulletPrefab => _bulletPrefab;

    [SerializeField] private Bullet _bulletPrefab;
}
