using System;
using System.Collections.Generic;
using UnityEngine;

public class SphereOverlap : Overlap
{
    [SerializeField] private float _radius;
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
        _radius = radius;
        return GetTargets<T>(GetColliders());
    }

    public override Collider[] GetColliders() => Physics.OverlapSphere(transform.position, _radius, _layerMask);

    public override void DrawOverlap()
    {
        if (IsDraw == false)
            return;

        Gizmos.color = DrawColor;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
