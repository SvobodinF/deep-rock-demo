using System.Collections.Generic;
using UnityEngine;

public class CubeOverlap : Overlap
{
    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector3 _size;
    [SerializeField] private LayerMask _layerMask;

    public override Transform GetNearestTarget()
    {
        return GetNearestTarget(GetTargets<Transform>());
    }

    public override T GetTarget<T>()
    {
        return GetTarget<T>(GetColliders());
    }

    public override List<T> GetTargets<T>()
    {
        return GetTargets<T>(GetColliders());
    }

    public override List<T> GetTargets<T>(float radius)
    {
        return GetTargets<T>(GetColliders());
    }

    public List<T> GetTargets<T>(Vector3 center, Vector3 size)
    {
        _center = center;
        _size = size;
        return GetTargets<T>(GetColliders());
    }

    public override Collider[] GetColliders() => Physics.OverlapBox(_center, _size, Quaternion.identity, _layerMask);

    public override void DrawOverlap()
    {
        if (IsDraw == false)
            return;

        Gizmos.color = DrawColor;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}
