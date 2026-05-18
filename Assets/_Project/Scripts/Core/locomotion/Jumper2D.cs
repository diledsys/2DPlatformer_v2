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
        if (_groundChecker.IsGrounded == false)
            return;

        _rigidbody.linearVelocity = new Vector2(
            _rigidbody.linearVelocity.x,
            _jumpForce
        );
    }
}