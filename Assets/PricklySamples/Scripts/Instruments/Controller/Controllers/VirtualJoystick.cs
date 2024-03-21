using UnityEngine;

public class VirtualJoystick : Controller
{
    [SerializeField] private VariableJoystick _variableJoystick;
    protected override Vector2 GetDirection() => IsEnabled == true ? _variableJoystick.Direction : Vector2.zero;
}
