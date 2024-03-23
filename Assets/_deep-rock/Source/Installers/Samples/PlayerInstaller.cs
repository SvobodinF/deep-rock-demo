using System;
using UnityEngine;

public class PlayerInstaller : Installer<PlayerConfiguration>
{
    public event Action<IAliveble> OnPlayerInstalledEvent;

    [Header("References")]
    [SerializeField] private MainCamera _mainCamera;
    [SerializeField] private Transform _spawnPoint;

    [Header("Prefabs")]
    [SerializeField] private Player _player;
    
    public override void Install(PlayerConfiguration playerConfiguration)
    {
        Player player = Instantiate(_player);
        IController controller = Instantiate(playerConfiguration.InputController);
        ObjectPool<Bullet> bulletPool = new ObjectPool<Bullet>(playerConfiguration.ShootingWeaponConfiguration.BulletPrefab, null, 100);
        PlayerData playerData = new PlayerData(playerConfiguration, controller, bulletPool);

        OnPlayerInstalledEvent?.Invoke(player);

        player.transform.position = _spawnPoint.position;
        player.Init(playerData, playerConfiguration);
        _mainCamera.Init(player);
    }
}
