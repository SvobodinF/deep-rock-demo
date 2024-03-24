public class DieAnimationParameter : IAnimationParameter
{
    public string AnimationName => "Dead";
    public AnimationCallType AnimationCallType => AnimationCallType.TRIGGER | AnimationCallType.BOOL;
}
