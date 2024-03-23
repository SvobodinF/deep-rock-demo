using UnityEngine;

public class InGameWindow : MonoBehaviour
{
    [SerializeField] private HealthBar _playerHealthBar;

    public void Init(IAliveble player)
    {
        _playerHealthBar.Init(player);
    }
}
