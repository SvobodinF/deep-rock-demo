using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Overlap2D
{
    protected readonly Transform Transform;
    protected readonly LayerMask LayerMask;

    public Overlap2D(Transform transform, LayerMask layerMask)
    {
        Transform = transform;
        LayerMask = layerMask;
    }

    public abstract Action DrawOverlap(Color color);
    public abstract List<T> GetTargets<T>();
    public abstract T GetTarget<T>();
    public abstract Transform GetNearestTarget();
    public abstract Collider2D[] GetColliders();

    protected T GetTarget<T>(Collider2D[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            T target = colliders[i].GetComponent<T>();

            if (target == null)
                continue;

            return target;
        }

        return default;
    }

    protected Transform GetNearestTarget(List<Transform> transforms)
    {
        Transform target = null;
        float distance = float.MaxValue;

        foreach (Transform transform in transforms)
        {
            float targetDistance = Vector3.Distance(transform.position, Transform.position);

            if (targetDistance < distance)
            {
                distance = targetDistance;
                target = transform;
            }
        }

        return target;
    }

    protected List<T> GetTargets<T>(Collider2D[] colliders)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < colliders.Length; i++)
        {
            T target = colliders[i].GetComponent<T>();

            if (target == null)
                continue;

            if (result.Contains(target) == true)
                continue;

            result.Add(target);
        }

        return result;
    }
}
