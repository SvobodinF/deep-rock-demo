using UnityEngine;

public class InGameWindow : MonoBehaviour
{
    [SerializeField] private HealthBar _playerHealthBar;
    [SerializeField] private OreContainer _oreContainer;

    public void Init(IAliveble player, OreHandler handler)
    {
        _playerHealthBar.Init(player);
        _oreContainer.Init(handler);
    }
}
