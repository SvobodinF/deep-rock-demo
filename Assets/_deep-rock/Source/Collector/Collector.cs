using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collector<T> : MonoBehaviour where T : ICollectable
{
    public event Action<T> OnCollectedEvent;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _radius;

    private Overlap2D _overlap2D;

    public void Init(ITransformable transformable)
    {
        _overlap2D = new CirculOverlap(transformable.transform, _layerMask, _radius);
    }

    private void Update()
    {
        Collect();
    }

    private void Collect()
    {
        List<T> targets = _overlap2D.GetTargets<T>();

        foreach (T target in targets)
        {
            if (target.CanCollected == false)
                continue;

            target.Collect();
            OnCollectedEvent?.Invoke(target);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
