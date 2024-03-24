using System;
using UnityEngine;

public class PlayerInstaller : Installer<PlayerInstallData>
{
    public event Action<IAliveble> OnPlayerInstalledEvent;

    [Header("References")]
    [SerializeField] private MainCamera _mainCamera;

    [Header("Prefabs")]
    [SerializeField] private Player _player;
    
    public override void Install(PlayerInstallData playerInstallData)
    {
        PlayerConfiguration configuration = playerInstallData.PlayerConfiguration;

        Player player = Instantiate(_player);
        IController controller = Instantiate(configuration.InputController);
        ObjectPool<Bullet> bulletPool = new ObjectPool<Bullet>(configuration.ShootingWeaponConfiguration.BulletPrefab, null, 100);
        PlayerData playerData = new PlayerData(configuration, controller, bulletPool);

        OnPlayerInstalledEvent?.Invoke(player);

        player.transform.position = playerInstallData.SpawnPosition;
        player.Init(playerData, configuration);
        _mainCamera.Init(player);
    }
}

public struct PlayerInstallData : IConfiguration
{
    public readonly PlayerConfiguration PlayerConfiguration;
    public readonly Vector3 SpawnPosition;

    public PlayerInstallData(PlayerConfiguration playerConfiguration, Vector3 spawnPosition)
    {
        PlayerConfiguration = playerConfiguration;
        SpawnPosition = spawnPosition;
    }
}
