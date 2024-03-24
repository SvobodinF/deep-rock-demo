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

        Target.Dig(worldPoint.Value, Damage);
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
            return Center.transform.position + new Vector3(direction.x, direction.y, 0f);
        }

        return null;
    }

    private RaycastHit2D GetRaycastHit(Vector2 direction)
    {
        Debug.DrawRay(Center.position, Direction * Radius, Color.green);
        return Physics2D.Raycast(Center.position, direction, Radius, LayerMask);
    }
}
