using UnityEngine;

public interface IController
{
    public Vector2 Direction { get; }
    public Quaternion Rotate(Vector3 from, Vector3 to) => Quaternion.FromToRotation(from, to);
    public Quaternion Rotate(Vector3 direction) => Quaternion.FromToRotation(Vector3.forward, direction);

    public bool IsEnabled { set; get; }
}
