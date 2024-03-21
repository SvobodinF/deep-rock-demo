using UnityEngine;

[CreateAssetMenu(fileName = nameof(ShootingWeaponConfiguration), menuName = "Configurations/Actors/Weapon/ShootingWeaponConfiguration", order = 51)]
public class ShootingWeaponConfiguration : Configuration
{
    public Bullet BulletPrefab => _bulletPrefab;
    public int Damage => _damage;
    public float ActionDelay => _actionDelay;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _actionDelay;
}
