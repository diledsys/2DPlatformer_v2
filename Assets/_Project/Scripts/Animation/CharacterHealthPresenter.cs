using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterAnimationPresenter))]
public class CharacterHealthPresenter : MonoBehaviour
{
    private Health _health;
    private CharacterAnimationPresenter _animationPresenter;
    private CharacterAttack _attack;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _animationPresenter = GetComponent<CharacterAnimationPresenter>();
        _attack = GetComponent<CharacterAttack>();
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