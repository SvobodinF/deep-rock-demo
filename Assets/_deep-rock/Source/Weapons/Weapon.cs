using System.Collections;
using UnityEngine;

public abstract class Weapon<T> : MonoBehaviour where T : ITransformable
{
    [SerializeField] protected LayerMask LayerMask;

    [Header("Debug")]
    [SerializeField] protected bool IsDraw;
    [SerializeField] protected Color DrawColor = Color.white;

    public bool HasTarget => Target != null;
    public Vector3 TargetPosition => Target.transform.position;

    [NaughtyAttributes.ShowNativeProperty] protected abstract T Target { get; }

    protected int Damage { get; private set; }
    private float _delay;
    private Coroutine _attackRoutine;

    public virtual void Init(int damage, float delay)
    {
        Damage = damage;
        _delay = delay;

        if (_attackRoutine != null)
            return;

        _attackRoutine = StartCoroutine(WaitAttackAction());
    }

    protected abstract void OnAttack();

    protected virtual void OnDrawGizmos()
    {
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
