using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private AttackHitbox _attackHitbox;
    [SerializeField] private CharacterAnimationPresenter _animationPresenter;

    private bool _isAttacking;

    public bool IsAttacking => _isAttacking;

    private void Awake()
    {
        if (_animationPresenter == null)
            _animationPresenter = GetComponent<CharacterAnimationPresenter>();

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