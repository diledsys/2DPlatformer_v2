using System.Collections;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField] private float _visionRadius = 5f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _checkInterval = 0.1f;

    private Coroutine _searchCoroutine;
    private WaitForSeconds _waitForSeconds;
    private Transform _target;

    public bool HasTarget => _target != null;
    public Transform Target => _target;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_checkInterval);
    }

    private void OnEnable()
    {
        _searchCoroutine = StartCoroutine(SearchRoutine());
    }

    private void OnDisable()
    {
        if (_searchCoroutine != null)
            StopCoroutine(_searchCoroutine);

        _searchCoroutine = null;
        _target = null;
    }

    private IEnumerator SearchRoutine()
    {
        while (enabled)
        {
            SearchTarget();
            yield return _waitForSeconds;
        }
    }

    private void SearchTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            _visionRadius,
            _targetLayer
        );

        if (hit == null)
        {
            _target = null;
            return;
        }

        if (hit.TryGetComponent(out PlayerTarget playerTarget) == false)
        {
            _target = null;
            return;
        }

        _target = playerTarget.transform;
    }

    private void OnValidate()
    {
        if (_checkInterval <= 0)
            _checkInterval = 0.1f;

        if (_visionRadius < 0)
            _visionRadius = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _visionRadius);
    }
}