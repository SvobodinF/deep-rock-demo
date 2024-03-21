using System.Collections.Generic;
using UnityEngine;

public abstract class Overlap : MonoBehaviour
{
    [SerializeField] protected bool IsDraw;
    [SerializeField] protected Color DrawColor = Color.white;

    public abstract void DrawOverlap();
    public abstract List<T> GetTargets<T>();
    public abstract List<T> GetTargets<T>(float radius);
    public abstract T GetTarget<T>();
    public abstract Transform GetNearestTarget();
    public abstract Collider[] GetColliders();

    protected T GetTarget<T>(Collider[] colliders)
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

        for (int i = 0; i < transforms.Count; i++)
        {
            float targetDistance = Vector3.Distance(transforms[i].position, transform.position);

            if (targetDistance < distance)
            {
                distance = targetDistance;
                target = transforms[i];
            }
        }

        return target;
    }

    protected List<T> GetTargets<T>(Collider[] colliders)
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

    private void OnDrawGizmos()
    {
        DrawOverlap();
    }
}
