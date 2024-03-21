using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : Movement
{
    [ShowNonSerializedField] private Rigidbody2D _rigidbody2D;

    public override Vector3 GetRotationByVelocity()
    {
        if (_rigidbody2D.velocity.x == 0)
            return new Vector3(0f, transform.eulerAngles.y, 0f);

        float rotation = Direction.x > 0 ? 0f : 180f;
        return new Vector3(0f, rotation, 0f);
    }

    protected override void Move(Vector3 direction)
    {
        _rigidbody2D.velocity = new Vector2(GetDirection(direction.x), GetDirection(direction.y));
    }

    protected override void OnValidate()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private float GetDirection(float axis)
    {
        return axis * Speed * Time.deltaTime;
    }
}
