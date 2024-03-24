using UnityEngine;

public abstract class Controller : MonoBehaviour, IController
{
    public Vector2 Direction => GetDirection();

    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            gameObject.SetActive(value);
        }
    }
    private bool _isEnabled;

    protected abstract Vector2 GetDirection();
}
