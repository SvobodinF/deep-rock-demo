using UnityEngine;

public abstract class MeleeWeapon<T> : Weapon<T> where T : ITransformable
{
    [SerializeField] protected Transform Center;
    protected float Radius;

    private Overlap2D _overlap2D;

    public void Init(int damage, float delay, float distance)
    {
        Radius = distance;
        _overlap2D = new CirculOverlap(Center, LayerMask, Radius);

        base.Init(damage, delay);
    }

    protected T[] FindAllTargets()
    {
        return _overlap2D.GetTargets<T>().ToArray();
    }
}
