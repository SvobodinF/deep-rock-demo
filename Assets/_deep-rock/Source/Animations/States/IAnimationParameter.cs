public interface IAnimationParameter
{
    public abstract string AnimationName { get; }
    public abstract AnimationCallType AnimationCallType { get; }
}

public enum AnimationCallType
{
    BOOL,
    TRIGGER,
    FLOAT,
    INT
}
