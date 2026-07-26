using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnockbackReceiver : MonoBehaviour
{
    [SerializeField] private float _verticalForceMultiplier = 0.3f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (force <= 0)
            return;

        Vector2 normalizedDirection = direction.normalized;

        normalizedDirection.y = Mathf.Abs(normalizedDirection.y) * _verticalForceMultiplier;
        normalizedDirection.Normalize();

        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.AddForce(normalizedDirection * force, ForceMode2D.Impulse);
    }
}