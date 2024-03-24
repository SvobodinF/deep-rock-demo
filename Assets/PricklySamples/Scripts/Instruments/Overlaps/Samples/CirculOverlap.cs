using System;
using System.Collections.Generic;
using UnityEngine;

public class CirculOverlap : Overlap2D
{
    private float _radius;

    public CirculOverlap(Transform transform, LayerMask layerMask, float radius) : base(transform, layerMask)
    {
        _radius = radius;
    }

    public override T GetNearestTarget<T>()
    {
        return GetNearestTarget(GetTargets<T>());
    }

    public override T GetTarget<T>()
    {
        return GetTarget<T>(GetColliders());
    }

    public override List<T> GetTargets<T>()
    {
        return GetTargets<T>(GetColliders());
    }

    public List<T> GetTargetsByRadius<T>(float radius)
    {
        _radius = radius;
        return GetTargets<T>(GetColliders());
    }

    public override Collider2D[] GetColliders() => Physics2D.OverlapCircleAll(Transform.position, _radius, LayerMask);

    public override Action DrawOverlap(Color color)
    {
        return () =>
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(Transform.position, _radius);
        };
    }
}
