using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(DamageDealer))]
public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private Health _ownerHealth;

    private Collider2D _collider;
    private DamageDealer _damageDealer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _damageDealer = GetComponent<DamageDealer>();

        _collider.isTrigger = true;
        _collider.enabled = false;
    }

    public void Enable()
    {
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health targetHealth) == false)
            return;

        if (targetHealth == _ownerHealth)
            return;

        targetHealth.TakeDamage(_damageDealer.Damage);
    }

    public void Disable()
    {
        _collider.enabled = false;
    }

}