using UnityEngine;

public abstract class Installer<T> : MonoBehaviour where T : Configuration
{
    public abstract void Install(T configuration);
}
