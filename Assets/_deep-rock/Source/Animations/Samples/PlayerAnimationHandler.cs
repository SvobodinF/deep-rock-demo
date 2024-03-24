using System;
using System.Collections.Generic;

public class PlayerAnimationHandler : AnimationHandler
{
    protected override void OnInit()
    {
        Animations = new Dictionary<Type, IAnimationParameter>()
        {
            { typeof(SpeedAnimationParameter), new SpeedAnimationParameter() },
            { typeof(DieAnimationParameter), new DieAnimationParameter() },
        };
    }
}
