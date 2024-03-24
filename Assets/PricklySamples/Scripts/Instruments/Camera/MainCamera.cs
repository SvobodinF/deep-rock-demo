using System;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Range(0, 1)] public float _followSmooth;
    [SerializeField] private Vector3 _followOffset;

    private bool _canFollowing;
    private Vector3 _offset;
    private ICameraTarget _target;

    public void Init(ICameraTarget target)
    {
        _target = target;
        _offset = _followOffset;

        _canFollowing = true;
    }

    private void FixedUpdate()
    {
        if (_canFollowing)
            Follow();
    }

    private void Follow()
    {
        if (_target.IsAlive)
            transform.position = Vector3.Lerp(transform.position, GetFollowPosition(_target, _offset), _followSmooth);
    }

    private Vector3 GetFollowPosition(ICameraTarget target, Vector3 offset) =>
        target.transform.position + offset;

    [NaughtyAttributes.Button]
    private void UpdateFollowOffset()
    {
        _offset = _followOffset;
    }
}
