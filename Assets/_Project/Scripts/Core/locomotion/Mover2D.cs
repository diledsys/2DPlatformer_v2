using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover2D : MonoBehaviour
{
    private const float DirectionDeadZone = 0.01f;

    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeedMultiplier = 1.6f;

    private Rigidbody2D _rigidbody;
    private float _direction;
    private float _facingDirection = 1f;
    private bool _isRunning;

    public float Direction => _direction;
    public float FacingDirection => _facingDirection;
    public float CurrentSpeed => Mathf.Abs(_rigidbody.linearVelocity.x);
    public bool IsRunning => _isRunning;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float speed = _isRunning ? _walkSpeed * _runSpeedMultiplier : _walkSpeed;

        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = _direction * speed;

        _rigidbody.linearVelocity = velocity;
    }

    public void SetDirection(float direction)
    {
        _direction = Mathf.Clamp(direction, -1f, 1f);

        if (Mathf.Abs(_direction) > DirectionDeadZone)
            _facingDirection = Mathf.Sign(_direction);
    }

    public void SetFacingDirection(float direction)
    {
        if (Mathf.Abs(direction) <= DirectionDeadZone)
            return;

        _facingDirection = Mathf.Sign(direction);
    }

    public void SetRunning(bool isRunning)
    {
        _isRunning = isRunning;
    }

    public void Stop()
    {
        _direction = 0f;
        _isRunning = false;
    }
}