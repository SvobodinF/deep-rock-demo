using UnityEngine;

public class PlayerInstaller : Installer<PlayerConfiguration>
{
    [Header("References")]
    [SerializeField] private MainCamera _mainCamera;
    [SerializeField] private Transform _spawnPoint;

    [Header("Prefabs")]
    [SerializeField] private Player _player;
    
    public override void Install(PlayerConfiguration playerConfiguration)
    {
        Player player = Instantiate(_player);
        player.transform.position = _spawnPoint.position;
        IController controller = Instantiate(playerConfiguration.InputController);

        PlayerData playerData = new PlayerData(controller, playerConfiguration.MovementConfiguration,
            playerConfiguration.ShootingWeaponConfiguration);

        player.Init(playerData);
        _mainCamera.Init(player);
    }
}
