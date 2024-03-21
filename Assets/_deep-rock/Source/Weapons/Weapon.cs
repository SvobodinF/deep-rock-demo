using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected LayerMask LayerMask;

    [Header("Debug")]
    [SerializeField] protected bool IsDraw;
    [SerializeField] protected Color DrawColor = Color.white;

    protected virtual bool CanAttack { get => false; }

    private int _damage;
    private float _delay;
    private Coroutine _attackRoutine;

    public void Init(int damage, float delay)
    {
        _damage = damage;
        _delay = delay;

        if (_attackRoutine != null)
            return;

        _attackRoutine = StartCoroutine(WaitAttackAction());
    }

    protected abstract void OnAttack(int damage);

    protected virtual void OnGizmosDebug()
    {
    }

    private void OnDrawGizmos()
    {
        OnGizmosDebug();
    }

    private IEnumerator WaitAttackAction()
    {
        while (true)
        {
            if (CanAttack == false)
                continue;

            OnAttack(_damage);

            yield return new WaitForSeconds(_delay);
        }
    }
}
