using UnityEngine;

public interface IDiggable : ITransformable
{
    public void Dig(Vector3 point, int damage);
}
