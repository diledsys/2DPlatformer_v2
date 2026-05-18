using UnityEngine;

public class GroundChecker2D : MonoBehaviour
{
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private float _radius = 0.15f;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(_checkPoint.position, _radius, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (_checkPoint == null)
            return;

        Gizmos.DrawWireSphere(_checkPoint.position, _radius);
    }
}