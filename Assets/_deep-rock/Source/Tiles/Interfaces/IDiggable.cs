using UnityEngine;

public interface IDiggable : ITransformable
{
    public void Dig(Vector3Int point, int damage);
}
