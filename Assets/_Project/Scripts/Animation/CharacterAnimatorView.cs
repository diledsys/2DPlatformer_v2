using UnityEngine;

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
    [SerializeField] private Transform _visualRoot;

    [Header("Settings")]
    [SerializeField] private bool _rotateByDirection = true;
    [SerializeField] private bool _facesRightByDefault = true;

    private Quaternion _startVisualRotation;
    private float _defaultFacingMultiplier;

    private void Awake()
    {
        if (_visualRoot != null)
            _startVisualRotation = _visualRoot.localRotation;

        _defaultFacingMultiplier = _facesRightByDefault ? 1f : -1f;
    }

    public void SetMovement(float horizontalSpeed, float verticalSpeed, bool isGrounded)
    {
        if (_animator == null)
            return;

        _animator.SetFloat(SpeedHash, horizontalSpeed);
        _animator.SetFloat(VerticalHash, verticalSpeed);
        _animator.SetBool(IsGroundedHash, isGrounded);
    }

    public void SetFacingDirection(float facingDirection)
    {
        if (_rotateByDirection == false)
            return;

        if (_visualRoot == null)
            return;

        float direction = facingDirection * _defaultFacingMultiplier;

        Quaternion rotation = direction >= 0
            ? _startVisualRotation
            : _startVisualRotation * Quaternion.Euler(0f, 180f, 0f);

        _visualRoot.localRotation = rotation;
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
}