using UnityEngine;
using UnityEngine.UI;

public class OreCounter : Counter
{
    [SerializeField] private Image _icon;

    public void Init(Sprite sprite)
    {
        _icon.sprite = sprite;
    }
}
