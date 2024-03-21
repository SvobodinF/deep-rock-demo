using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
[RequireComponent(typeof(PlayerAnimationHandler))]
public class Player : Actor<PlayerData>, ICameraTarget
{
    //set here health checker
    public bool IsAlive => true;

    [ShowNonSerializedField] private Movement _movement;

    protected override void OnInit(PlayerData data)
    {
        OnValidate();

        _movement.Init(data.Controller, data.MovementConfiguration);
        AnimationHandler.Init();
    }

    private void OnValidate()
    {
        _movement = GetComponent<Movement>();
    }

    protected override void Rotate()
    {
        if (_movement.Direction.x == 0 && _movement.Direction.y == 0)
        {
            return;
        }
        else
        {
            transform.eulerAngles = _movement.GetRotationByVelocity();
        }

        

        /*if (_shooting.Target == null)
        {
            /*if (isDie == true)
            {
                transform.localScale = new Vector3(_localScaleValue, _localScaleValue, _localScaleValue);
                return;
            }

            if (_movement2D.Direction.x == 0)
            {
                return;
            }
            else
            {
                transform.eulerAngles = _movement2D.Rotation;
            }

            return;
        }*/

        //bool isRight = _shooting.Target.position.x >= transform.position.x;
        //float angle = isRight ? 0f : 180f;
        //print(angle);

        //transform.eulerAngles = _movement.GetRotationByVelocity();

        /*if (_movement2D.Direction.x == 0)
        {
            _animationController.Set(AnimationController.StateName.IsBack, AnimationController.Option.BOOL, false);
            return;
        }

        bool backShooting = isRight ? _movement2D.Direction.x < 0 ? true : false : _movement2D.Direction.x < 0 ? false : true;

        _animationController.Set(AnimationController.StateName.IsBack, AnimationController.Option.BOOL, backShooting);*/
    }

    protected override void OnAnimate()
    {
        AnimationHandler.Set<SpeedAnimationParameter, float>(_movement.Velocity);
    }
}

public struct PlayerData
{
    public readonly IController Controller;
    public readonly MovementConfiguration MovementConfiguration;

    public PlayerData(IController controller, MovementConfiguration movementConfiguration)
    {
        Controller = controller;
        MovementConfiguration = movementConfiguration;
    }
}