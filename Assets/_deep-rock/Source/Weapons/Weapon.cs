using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected LayerMask LayerMask;

    [Header("Debug")]
    [SerializeField] protected bool IsDraw;
    [SerializeField] protected Color DrawColor = Color.white;

    public bool HasTarget => Target != null;
    public Vector3 TargetPosition => Target.position;

    protected abstract Transform Target { get; }

    protected int Damage { get; private set; }
    private float _delay;
    private Coroutine _attackRoutine;

    public void Init(int damage, float delay)
    {
        Damage = damage;
        _delay = delay;

        if (_attackRoutine != null)
            return;

        _attackRoutine = StartCoroutine(WaitAttackAction());
    }

    protected abstract void OnAttack();

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
            yield return new WaitUntil(() => HasTarget);

            OnAttack();

            yield return new WaitForSeconds(_delay);
        }
    }
}
