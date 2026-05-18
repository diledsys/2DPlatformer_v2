using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterAnimationPresenter : MonoBehaviour
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
    [SerializeField] private bool _flipByDirection = true;
    [SerializeField] private bool _facesRightByDefault = true;

    private Rigidbody2D _rigidbody;
    private Vector3 _startVisualScale;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();

        if (_groundChecker == null)
            _groundChecker = GetComponent<GroundChecker2D>();

        if (_mover == null)
            _mover = GetComponent<Mover2D>();

        if (_visualRoot != null)
            _startVisualScale = _visualRoot.localScale;
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
        if (_flipByDirection == false)
            return;

        if (_visualRoot == null || _mover == null)
            return;

        float direction = _mover.FacingDirection;

        if (_facesRightByDefault == false)
            direction *= -1f;

        Vector3 scale = _startVisualScale;
        scale.x = Mathf.Abs(_startVisualScale.x) * direction;

        _visualRoot.localScale = scale;
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

        _animator.SetTrigger(DeadHash);
    }
}