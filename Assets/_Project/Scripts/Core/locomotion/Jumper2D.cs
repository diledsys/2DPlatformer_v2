using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker2D))]
public class Jumper2D : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 8f;

    private Rigidbody2D _rigidbody;
    private GroundChecker2D _groundChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker2D>();
    }

    public void Jump()
    {
        _groundChecker.CheckNow();

        if (_groundChecker.IsGrounded == false)
            return;

        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.y = _jumpForce;

        _rigidbody.linearVelocity = velocity;
    }
}