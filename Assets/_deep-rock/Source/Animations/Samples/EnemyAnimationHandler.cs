using System;
using System.Collections.Generic;

public class EnemyAnimationHandler : AnimationHandler
{
    protected override void OnInit()
    {
        Animations = new Dictionary<Type, IAnimationParameter>()
        {
            { typeof(HitAnimationParameter), new HitAnimationParameter() },
            { typeof(DieAnimationParameter), new DieAnimationParameter() },
        };
    }
}
