using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover2D))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(EnemySight))]
public class EnemyBehaviour : MonoBehaviour
{
    private const float TargetReachedDeadZone = 0.1f;

    [Header("Patrol")]
    [SerializeField] private float _patrolChangeDirectionDelay = 2f;

    [Header("Combat")]
    [SerializeField] private float _attackDistance = 1.8f;

    private Mover2D _mover;
    private MeleeAttack _attack;
    private EnemySight _sight;

    private Coroutine _patrolCoroutine;
    private WaitForSeconds _patrolWait;
    private float _patrolDirection = 1f;

    private void Awake()
    {
        _mover = GetComponent<Mover2D>();
        _attack = GetComponent<MeleeAttack>();
        _sight = GetComponent<EnemySight>();

        _patrolWait = new WaitForSeconds(_patrolChangeDirectionDelay);
    }

    private void OnEnable()
    {
        _patrolCoroutine = StartCoroutine(PatrolRoutine());
    }

    private void OnDisable()
    {
        if (_patrolCoroutine != null)
            StopCoroutine(_patrolCoroutine);

        _patrolCoroutine = null;
    }

    private void Update()
    {
        if (_sight.HasTarget == false)
        {
            Patrol();
            return;
        }

        ChaseOrAttack(_sight.Target);
    }

    private IEnumerator PatrolRoutine()
    {
        while (enabled)
        {
            yield return _patrolWait;
            _patrolDirection *= -1f;
        }
    }

    private void Patrol()
    {
        _mover.SetDirection(_patrolDirection);
    }

    private void ChaseOrAttack(Transform target)
    {
        if (target == null)
        {
            Patrol();
            return;
        }

        float horizontalDistance = Mathf.Abs(target.position.x - transform.position.x);
        float directionToTarget = Mathf.Sign(target.position.x - transform.position.x);

        if (horizontalDistance <= _attackDistance)
        {
            _mover.Stop();
            _mover.SetFacingDirection(directionToTarget);
            _attack.Attack();
            return;
        }

        if (horizontalDistance <= TargetReachedDeadZone)
        {
            _mover.Stop();
            return;
        }

        _mover.SetDirection(directionToTarget);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}