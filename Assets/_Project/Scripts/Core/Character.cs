using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Mover2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MeleeAttack))]
public class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterAnimatorView _animatorView;
    [SerializeField] private GroundChecker2D _groundChecker;

    private Rigidbody2D _rigidbody;
    private Mover2D _mover;
    private Health _health;
    private MeleeAttack _meleeAttack;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _mover = GetComponent<Mover2D>();
        _health = GetComponent<Health>();
        _meleeAttack = GetComponent<MeleeAttack>();
    }

    private void OnEnable()
    {
        _health.Damaged += OnDamaged;
        _health.Died += OnDied;
    }

    private void LateUpdate()
    {
        UpdateAnimationState();
    }
   
    private void OnDisable()
    {
        _health.Damaged -= OnDamaged;
        _health.Died -= OnDied;
    }

    public void Attack()
    {
        if (_health.IsDead)
            return;

        if (_meleeAttack.TryStart() == false)
            return;

        _animatorView.PlayAttack();
    }

    public void OpenAttackHitbox()
    {
        _meleeAttack.OpenHitbox();
    }

    public void CloseAttackHitbox()
    {
        _meleeAttack.CloseHitbox();
    }

    public void FinishAttack()
    {
        _meleeAttack.FinishAttack();
    }

    private void UpdateAnimationState()
    {
        if (_animatorView == null)
            return;

        float horizontalSpeed = Mathf.Abs(_rigidbody.linearVelocity.x);
        float verticalSpeed = _rigidbody.linearVelocity.y;
        bool isGrounded = _groundChecker != null && _groundChecker.IsGrounded;

        _animatorView.SetMovement(horizontalSpeed, verticalSpeed, isGrounded);
        _animatorView.SetFacingDirection(_mover.FacingDirection);
    }

    private void OnDamaged()
    {
        if (_health.IsDead)
            return;

        _meleeAttack.Interrupt();
        _animatorView.PlayDamage();
    }

    private void OnDied()
    {
        _meleeAttack.Interrupt();
        _animatorView.PlayDead();
    }
}