using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyBonePart : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        DisablePhysics();
    }

    public void EnablePhysics(Vector2 force, float torque)
    {
        transform.SetParent(null);

        _collider.enabled = true;

        _rigidbody.simulated = true;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.gravityScale = 1f;
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;

        _rigidbody.AddForce(force, ForceMode2D.Impulse);
        _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
    }

    private void DisablePhysics()
    {
        _collider.enabled = false;
        _rigidbody.simulated = false;
    }
}