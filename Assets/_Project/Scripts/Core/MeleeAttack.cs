using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private AttackHitbox _hitbox;

    private bool _isAttacking;

    public bool IsAttacking => _isAttacking;

    private void Awake()
    {
        if (_hitbox == null)
            Debug.LogError($"{name}: DamageHitbox reference is missing");
    }

    public bool TryStart()
    {
        if (_isAttacking)
            return false;

        _isAttacking = true;
        return true;
    }

    public void OpenHitbox()
    {
        if (_hitbox != null)
            _hitbox.Enable();
    }

    public void CloseHitbox()
    {
        if (_hitbox != null)
            _hitbox.Disable();
    }

    public void FinishAttack()
    {
        CloseHitbox();
        _isAttacking = false;
    }

    public void Interrupt()
    {
        CloseHitbox();
        _isAttacking = false;
    }
}