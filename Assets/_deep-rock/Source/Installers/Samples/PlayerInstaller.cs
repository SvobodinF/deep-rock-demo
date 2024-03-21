using UnityEngine;

public class PlayerInstaller : Installer<PlayerConfiguration>
{
    [Header("References")]
    [SerializeField] private MainCamera _mainCamera;

    [Header("Prefabs")]
    [SerializeField] private Player _player;
    
    public override void Install(PlayerConfiguration playerConfiguration)
    {
        Player player = Instantiate(_player);
        IController controller = Instantiate(playerConfiguration.InputController);

        PlayerData playerData = new PlayerData(controller, playerConfiguration.MovementConfiguration);

        player.Init(playerData);
        _mainCamera.Init(player);
    }
}
