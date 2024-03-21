using UnityEngine;
using NaughtyAttributes;

public abstract class Actor<T> : MonoBehaviour where T : struct
{
    [SerializeField] protected Transform Model;
    [ShowNonSerializedField] protected AnimationHandler AnimationHandler;

    public void Init(T configuration)
    {
        OnValidate();
        OnInit(configuration);
    }

    protected abstract void OnInit(T configuration);
    protected abstract void Rotate();
    protected abstract void OnAnimate();

    private void Update()
    {
        Rotate();
        OnAnimate();
    }

    private void OnValidate()
    {
        AnimationHandler = GetComponent<AnimationHandler>();
    }
}
