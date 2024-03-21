using NaughtyAttributes;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public bool CanMove => Controller != null && Controller.IsEnabled == true && _canMove;

    [SerializeField] protected bool _canMove = true;
    public IController Controller { get; protected set; }

    [Header("Stats")]
    [ShowNonSerializedField] protected float MaxSpeed;
    [SerializeField] protected float Speed;

    public float Velocity => Mathf.Max(Mathf.Abs(Controller.Direction.x), Mathf.Abs(Controller.Direction.y));
    public virtual Vector3 Direction => new Vector3(Controller.Direction.x, 0f, Controller.Direction.y);

    public virtual void SetSpeed(float percent)
    {
        Speed = MaxSpeed * percent;
    }

    public abstract Vector3 GetRotationByVelocity();
    protected abstract void Move(Vector3 direction);
    protected abstract void OnValidate();

    public virtual void Stop()
    {
        _canMove = false;
    }

    public virtual void Play()
    {
        _canMove = true;
    }

    public void Init(IController controller, MovementConfiguration configuration)
    {
        OnValidate();

        SetController(controller);

        MaxSpeed = configuration.MaxSpeed;
        Speed = MaxSpeed;
    }

    protected void SetController(IController controller)
    {
        Controller = controller;
        Controller.IsEnabled = true;
    }

    private void FixedUpdate()
    {
        if (CanMove == false)
        {
            Stop();
            return;
        }

        Move(Direction); 
    }
}