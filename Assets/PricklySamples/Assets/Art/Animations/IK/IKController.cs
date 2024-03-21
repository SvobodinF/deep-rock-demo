using System;
using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private Animator _animator;

    [Header("Settings")]
    [SerializeField] private IKRoot[] _iKRoots;
    public IKRoot[] IKRoots
    {
        get
        {
            return _iKRoots;
        }
        set
        {
            _iKRoots = value;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isActive == false)
            return;

        foreach (IKRoot iKRoot in _iKRoots)
        {
            _animator.SetLayerWeight(1, iKRoot.LayerWeight);
            _animator.SetIKPositionWeight(iKRoot.AvatarIKGoal, iKRoot.Weight);
            _animator.SetIKRotationWeight(iKRoot.AvatarIKGoal, iKRoot.Weight);
            _animator.SetIKPosition(iKRoot.AvatarIKGoal, iKRoot.RootTransform.position);
            _animator.SetIKRotation(iKRoot.AvatarIKGoal, iKRoot.RootTransform.rotation);
        }
    }

    [Serializable]
    public struct IKRoot
    {
        [SerializeField] private AvatarIKGoal _avatarIKGoal;
        [SerializeField] private Transform _rootTransform;

        [Header("Weight")]
        [SerializeField, Range(0, 1)] private float _ikWeight;
        [SerializeField, Range(0, 1)] private float _layerWeight;

        public AvatarIKGoal AvatarIKGoal => _avatarIKGoal;
        public Transform RootTransform => _rootTransform;
        public float Weight => _ikWeight;
        public float LayerWeight
        {
            get
            {
                return _layerWeight;
            }
            set
            {
                if (value < 0 || value > 1)
                    return;

                _layerWeight = value;
            }
        }


        public void SetWeight(float value)
        {
            _ikWeight = value;
        }
    }
}