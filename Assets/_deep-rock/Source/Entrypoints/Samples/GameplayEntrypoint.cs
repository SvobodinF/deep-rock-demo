using UnityEngine;

public class GameplayEntrypoint : Entrypoint
{
    [Header("Installers")]
    [SerializeField] private PlayerInstaller _playerInstaller;

    [Header("ScriptableObjects")]
    [SerializeField] private GameConfiguration _gameConfiguration;

    public override void Start()
    {
        _playerInstaller.Install(_gameConfiguration.PlayerConfiguration);
    }
}
