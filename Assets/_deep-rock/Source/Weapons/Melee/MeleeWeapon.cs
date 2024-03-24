using UnityEngine;

public abstract class MeleeWeapon<T> : Weapon<T> where T : class, ITransformable
{
    [SerializeField] protected Transform Center;
    protected float Radius;

    protected Overlap2D Overlap2D;

    public void Init(int damage, float delay, float distance)
    {
        Radius = distance;
        Overlap2D = new CirculOverlap(Center, LayerMask, Radius);

        base.Init(damage, delay);
    }
}
