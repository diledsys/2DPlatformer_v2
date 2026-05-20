using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterAnimatorView : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int VerticalHash = Animator.StringToHash("Vertical");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int DamageHash = Animator.StringToHash("Damage");
    private static readonly int DeadHash = Animator.StringToHash("Dead");

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private GroundChecker2D _groundChecker;
    [SerializeField] private Transform _visualRoot;
    [SerializeField] private Mover2D _mover;

    [Header("Settings")]
    [SerializeField] private bool _rotateByDirection = true;
    [SerializeField] private bool _facesRightByDefault = true;

    private Rigidbody2D _rigidbody;
    private Quaternion _startVisualRotation;
    private float _defaultFacingMultiplier;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null)
            Debug.LogError($"{name}: Animator reference is missing");

        if (_visualRoot == null)
            Debug.LogError($"{name}: VisualRoot reference is missing");

        if (_mover == null)
            Debug.LogError($"{name}: Mover2D reference is missing");

        if (_visualRoot != null)
            _startVisualRotation = _visualRoot.localRotation;

        _defaultFacingMultiplier = _facesRightByDefault ? 1f : -1f;
    }

    private void Update()
    {
        UpdateMovementAnimation();
        UpdateFacingDirection();
    }

    public void PlayAttack()
    {
        if (_animator == null)
            return;

        _animator.SetTrigger(AttackHash);
    }

    public void PlayDamage()
    {
        if (_animator == null)
            return;

        _animator.ResetTrigger(AttackHash);
        _animator.SetTrigger(DamageHash);
    }

    public void PlayDead()
    {
        if (_animator == null)
            return;

        _animator.ResetTrigger(AttackHash);
        _animator.ResetTrigger(DamageHash);
        _animator.SetTrigger(DeadHash);
    }

    private void UpdateMovementAnimation()
    {
        if (_animator == null)
            return;

        float horizontalSpeed = Mathf.Abs(_rigidbody.linearVelocity.x);
        float verticalSpeed = _rigidbody.linearVelocity.y;
        bool isGrounded = _groundChecker != null && _groundChecker.IsGrounded;

        _animator.SetFloat(SpeedHash, horizontalSpeed);
        _animator.SetFloat(VerticalHash, verticalSpeed);
        _animator.SetBool(IsGroundedHash, isGrounded);
    }

    private void UpdateFacingDirection()
    {
        if (_rotateByDirection == false)
            return;

        if (_visualRoot == null || _mover == null)
            return;

        float direction = _mover.FacingDirection * _defaultFacingMultiplier;

        Quaternion rotation = direction >= 0
            ? _startVisualRotation
            : _startVisualRotation * Quaternion.Euler(0f, 180f, 0f);

        _visualRoot.localRotation = rotation;
    }
}