using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private IAliveble _aliveble;

    public void Init(IAliveble aliveble)
    {
        _aliveble = aliveble;

        _aliveble.OnHeathPercentChangedEvent += ChangeFill;
    }

    private void OnDestroy()
    {
        _aliveble.OnHeathPercentChangedEvent -= ChangeFill;
    }

    private void ChangeFill(float percent)
    {
        _slider.value = percent;
    }
}
