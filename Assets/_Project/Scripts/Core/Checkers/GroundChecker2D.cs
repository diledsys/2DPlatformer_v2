using System.Collections;
using UnityEngine;

public class GroundChecker2D : MonoBehaviour
{
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private float _radius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkInterval = 0.1f;

    private Coroutine _checkingCoroutine;
    private WaitForSeconds _waitForSeconds;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        if (_checkPoint == null)
            _checkPoint = transform;

        _waitForSeconds = new WaitForSeconds(_checkInterval);
    }

    private void OnEnable()
    {
        _checkingCoroutine = StartCoroutine(CheckGroundRoutine());
    }

    private void OnDisable()
    {
        if (_checkingCoroutine != null)
            StopCoroutine(_checkingCoroutine);

        _checkingCoroutine = null;
    }

    public void CheckNow()
    {
        IsGrounded = Physics2D.OverlapCircle(
            _checkPoint.position,
            _radius,
            _groundLayer
        );
    }

    private IEnumerator CheckGroundRoutine()
    {
        while (enabled)
        {
            CheckNow();
            yield return _waitForSeconds;
        }
    }

    private void OnValidate()
    {
        if (_checkInterval <= 0)
            _checkInterval = 0.1f;
    }

    private void OnDrawGizmosSelected()
    {
        if (_checkPoint == null)
            return;

        Gizmos.DrawWireSphere(_checkPoint.position, _radius);
    }
}