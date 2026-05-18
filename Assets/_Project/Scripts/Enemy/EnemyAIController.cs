using UnityEngine;

[RequireComponent(typeof(Mover2D))]
[RequireComponent(typeof(CharacterAttack))]
public class EnemyAIController : MonoBehaviour
{
    private const float TargetReachedDeadZone = 0.1f;

    [Header("Patrol")]
    [SerializeField] private float _patrolChangeDirectionDelay = 2f;

    [Header("Detection")]
    [SerializeField] private float _visionRadius = 5f;
    [SerializeField] private float _attackDistance = 1.2f;
    [SerializeField] private LayerMask _targetLayer;

    [Header("Debug")]
    [SerializeField] private bool _showDebugLogs = true;

    private Mover2D _mover;
    private CharacterAttack _attack;

    private Transform _target;
    private float _patrolDirection = 1f;
    private float _patrolTimer;

    private void Awake()
    {
        _mover = GetComponent<Mover2D>();
        _attack = GetComponent<CharacterAttack>();
    }

    private void Update()
    {
        FindTarget();

        if (_target == null)
        {
            Patrol();
            return;
        }

        ChaseOrAttackTarget();
    }

    private void FindTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            _visionRadius,
            _targetLayer
        );

        if (hit == null)
        {
            if (_target != null && _showDebugLogs)
                Debug.Log($"{name}: lost target");

            _target = null;
            return;
        }

        if (hit.TryGetComponent(out PlayerTarget playerTarget) == false)
            return;

        if (_target == null && _showDebugLogs)
            Debug.Log($"{name}: target detected");

        _target = playerTarget.transform;
    }

    private void Patrol()
    {
        _patrolTimer += Time.deltaTime;

        if (_patrolTimer >= _patrolChangeDirectionDelay)
        {
            _patrolTimer = 0f;
            _patrolDirection *= -1f;
        }

        _mover.SetDirection(_patrolDirection);
    }

    private void ChaseOrAttackTarget()
    {
        float distanceToTarget = Vector2.Distance(transform.position, _target.position);

        if (distanceToTarget <= _attackDistance)
        {
            _mover.Stop();
            _attack.Attack();
            return;
        }

        float direction = Mathf.Sign(_target.position.x - transform.position.x);

        if (Mathf.Abs(_target.position.x - transform.position.x) <= TargetReachedDeadZone)
        {
            _mover.Stop();
            return;
        }

        _mover.SetDirection(direction);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _visionRadius);
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}