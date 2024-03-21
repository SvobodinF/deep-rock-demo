using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AnimationHandler : MonoBehaviour
{
    private delegate void AnimationValueChanging(string animationName, object value);  

    [SerializeField] private Animator _animator;

    protected Dictionary<Type, IAnimationParameter> Animation;

    public void Init()
    {
        OnInit();
    }

    protected abstract void OnInit();

    public void Set<T, V>(V value) where T : IAnimationParameter
    {
        SwitchAnimation<T>(value);
    }

    protected internal void SwitchAnimation<T>(object value = null)
    {
        IAnimationParameter animationState = GetAnimationByType(typeof(T));

        AnimationValueChanging valueChanging = animationState.AnimationCallType switch
        {
            AnimationCallType.BOOL => (name, value) => Bool(name, value),
            AnimationCallType.FLOAT => (name, value) => Float(name, value),
            AnimationCallType.INT => (name, value) => Int(name, value),
            AnimationCallType.TRIGGER => (name, value) => Trigger(name),
            _ => throw new NotImplementedException()
        };

        valueChanging?.Invoke(animationState.AnimationName, value);
    }

    private IAnimationParameter GetAnimationByType(Type type)
    {
        if (Animation.TryGetValue(type, out IAnimationParameter state))
        {
            return state;
        }

        throw new ArgumentNullException("Animation is not found");
    }

    private void Bool(string stateName, object value)
    {
        _animator.SetBool(stateName, Utils.Utils.Unboxing<bool>(value));
    }

    private void Trigger(string stateName)
    {
        _animator.SetTrigger(stateName);
    }

    private void Int(string stateName, object value)
    {
        _animator.SetInteger(stateName, Utils.Utils.Unboxing<int>(value));
    }

    private void Float(string stateName, object value)
    {
        _animator.SetFloat(stateName, Utils.Utils.Unboxing<float>(value));
    }
}
