using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterAnimatorView))]
public class CharacterDamageView : MonoBehaviour
{
    private Health _health;
    private CharacterAnimatorView _animationPresenter;
    private MeleeAttack _attack;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _animationPresenter = GetComponent<CharacterAnimatorView>();
        _attack = GetComponent<MeleeAttack>();
    }

    private void OnEnable()
    {
        _health.Damaged += OnDamaged;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Damaged -= OnDamaged;
        _health.Died -= OnDied;
    }

    private void OnDamaged()
    {
        if (_health.IsDead)
            return;

        if (_attack != null)
            _attack.FinishAttack();

        _animationPresenter.PlayDamage();
    }

    private void OnDied()
    {
        _animationPresenter.PlayDead();
    }
}