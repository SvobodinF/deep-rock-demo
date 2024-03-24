using UnityEngine;

public abstract class Ore : MonoBehaviour, ICollectable
{
    public bool CanCollected => _linkedTile == null;
    public Sprite Sprite => _sprite;

    [SerializeField] private Sprite _sprite;

    private Vector3Int? _linkedTile;

    public void SetLinkedTile(Vector3Int? tile)
    {
        _linkedTile = tile;
    }

    public void Collect()
    {
        if (CanCollected == false)
            return;

        gameObject.SetActive(false);
    }
}
