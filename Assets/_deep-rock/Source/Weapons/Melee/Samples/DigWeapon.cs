using UnityEngine;

public class DigWeapon : MeleeWeapon<IDiggable>
{
    protected override IDiggable Target => GetTarget();

    private IController _controller;
    private Vector2 Direction => _controller == null ? Vector2.zero : _controller.Direction;

    public void Init(IController controller, MeleeWeaponConfiguration meleeWeaponConfiguration)
    {
        _controller = controller;

        Init(meleeWeaponConfiguration.Damage, meleeWeaponConfiguration.ActionDelay, meleeWeaponConfiguration.Distance);
    }

    protected override void OnAttack()
    {
        if (HasTarget == false)
            return;

        Vector3? worldPoint = GetHitPointByDirection(Direction);

        if (worldPoint == null)
            return;

        Vector3Int point3Int = new Vector3Int((int)worldPoint.Value.x, (int)worldPoint.Value.y, (int)worldPoint.Value.z);
        Target.Dig(point3Int, Damage);
    }

    private IDiggable GetTarget()
    {
        RaycastHit2D hit = GetRaycastHit(Direction);

        if (hit.transform == null)
            return null;

        if (hit.transform.TryGetComponent(out IDiggable diggable))
        {
            return diggable;
        }

        return null;
    }

    private Vector3? GetHitPointByDirection(Vector2 direction)
    {
        RaycastHit2D hit = GetRaycastHit(direction);

        if (hit.transform.TryGetComponent(out IDiggable diggable))
        {
            return hit.point;
        }

        return null;
    }

    private RaycastHit2D GetRaycastHit(Vector2 direction)
    {
        return Physics2D.Raycast(Center.position, direction, Radius, LayerMask);
    }

    protected override void OnGizmosDebug()
    {
        if (_controller == null)
            return;

        Gizmos.DrawRay(Center.position, _controller.Direction);
    }
}
