using UnityEngine;

public class Keyboard : Controller
{
    [SerializeField] private KeyCode _up;
    [SerializeField] private KeyCode _down;
    [SerializeField] private KeyCode _left;
    [SerializeField] private KeyCode _right;

    protected override Vector2 GetDirection()
    {
        int vertical = 0;
        int horizontal = 0;

        if (Input.GetKey(_up) == true)
        {
            vertical = 1;
        }

        if (Input.GetKey(_down) == true)
        {
            vertical = -1;
        }

        if (Input.GetKey(_right) == true)
        {
            horizontal = 1;
        }

        if (Input.GetKey(_left) == true)
        {
            horizontal = -1;
        }

        return new Vector2(horizontal, vertical);
    }
}
