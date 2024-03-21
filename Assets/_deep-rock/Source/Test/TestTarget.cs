using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour, IDamageable
{
    public void OnDamage(float damage)
    {
        Debug.Log($"{nameof(TestTarget)} get damage {damage}");
    }
}
