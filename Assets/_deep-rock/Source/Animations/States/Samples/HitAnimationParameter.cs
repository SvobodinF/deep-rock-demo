public class HitAnimationParameter : IAnimationParameter
{
    public string AnimationName => "Hit";
    public AnimationCallType AnimationCallType => AnimationCallType.TRIGGER | AnimationCallType.BOOL;
}
