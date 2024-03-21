using System.Collections.Generic;
using UnityEngine;

public class CapsuleOverlap : Overlap
{
    [SerializeField] private Transform _startPoint; 
    [SerializeField] private Transform _endPoint;
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

    public override Collider[] GetColliders() => Physics.OverlapCapsule(_startPoint.position, _endPoint.position, _radius, _layerMask);

    public override void DrawOverlap()
    {
        if (IsDraw == false)
            return;

        DrawWireCapsule(_startPoint.position, _endPoint.position, _radius, DrawColor);
    }

    private void DrawWireCapsule(Vector3 p1, Vector3 p2, float radius, Color color)
    {
#if UNITY_EDITOR
        if (p1 == p2)
        {
            Gizmos.DrawWireSphere(p1, radius);
            return;
        }
        using (new UnityEditor.Handles.DrawingScope(Gizmos.color = color, Gizmos.matrix))
        {
            Quaternion p1Rotation = Quaternion.LookRotation(p1 - p2);
            Quaternion p2Rotation = Quaternion.LookRotation(p2 - p1);

            float c = Vector3.Dot((p1 - p2).normalized, Vector3.up);
            if (c == 1f || c == -1f)
            {
                p2Rotation = Quaternion.Euler(p2Rotation.eulerAngles.x, p2Rotation.eulerAngles.y + 180f, p2Rotation.eulerAngles.z);
            }

            UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.left, p1Rotation * Vector3.down, 180f, radius);
            UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.up, p1Rotation * Vector3.left, 180f, radius);
            UnityEditor.Handles.DrawWireDisc(p1, (p2 - p1).normalized, radius);

            UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.left, p2Rotation * Vector3.down, 180f, radius);
            UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.up, p2Rotation * Vector3.left, 180f, radius);
            UnityEditor.Handles.DrawWireDisc(p2, (p1 - p2).normalized, radius);

            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.down * radius, p2 + p2Rotation * Vector3.down * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.left * radius, p2 + p2Rotation * Vector3.right * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.up * radius, p2 + p2Rotation * Vector3.up * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.right * radius, p2 + p2Rotation * Vector3.left * radius);
        }
#endif
    }
}
