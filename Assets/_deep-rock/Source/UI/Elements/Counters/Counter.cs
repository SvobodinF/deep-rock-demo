using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counter;

    public virtual void SetCount(int count)
    {
        _counter.text = $"{count}";
    }
}
