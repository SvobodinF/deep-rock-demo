using System;
using System.Collections.Generic;

public class PlayerAnimationHandler : AnimationHandler
{
    protected override void OnInit()
    {
        Animation = new Dictionary<Type, IAnimationParameter>()
        {
            { typeof(SpeedAnimationParameter), new SpeedAnimationParameter() },
        };
    }
}
