using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private AttackHitbox _attackHitbox;
    [SerializeField] private CharacterAnimatorView _animationPresenter;

    private bool _isAttacking;

    public bool IsAttacking => _isAttacking;

    private void Awake()
    {
        if (_animationPresenter == null)
            _animationPresenter = GetComponent<CharacterAnimatorView>();

        if (_attackHitbox != null)
            _attackHitbox.Disable();
    }

    public void Attack()
    {
        if (_isAttacking)
            return;

        _isAttacking = true;

        if (_animationPresenter != null)
            _animationPresenter.PlayAttack();
    }

    public void OpenHitbox()
    {
        if (_attackHitbox != null)
            _attackHitbox.Enable();
    }

    public void CloseHitbox()
    {
        if (_attackHitbox != null)
            _attackHitbox.Disable();
    }

    public void FinishAttack()
    {
        CloseHitbox();
        _isAttacking = false;
    }
}