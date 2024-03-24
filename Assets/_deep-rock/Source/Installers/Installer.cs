using UnityEngine;

public abstract class Installer<T> : MonoBehaviour where T : IConfiguration
{
    public abstract void Install(T configuration);
}
