using NaughtyAttributes;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public bool CanMove => Controller != null && Controller.IsEnabled == true && _canMove && _aliveble.IsAlive;

    [SerializeField] protected bool _canMove = true;
    public IController Controller { get; protected set; }
    private IAliveble _aliveble;

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

    public void Init(IController controller, IAliveble aliveble, MovementConfiguration configuration)
    {
        OnValidate();

        SetController(controller);
        _aliveble = aliveble;

        _aliveble.OnDiedEvent += OnDie;

        MaxSpeed = configuration.MaxSpeed;
        Speed = MaxSpeed;
    }

    private void OnDestroy()
    {
        _aliveble.OnDiedEvent -= OnDie;
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
            return;
        }

        Move(Direction); 
    }

    private void OnDie()
    {
        _canMove = false;
        Controller.IsEnabled = false;
    }
}