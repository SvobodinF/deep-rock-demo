using UnityEngine;

public interface ICameraTarget
{
    bool IsAlive { get; }
    Transform transform { get; }
}