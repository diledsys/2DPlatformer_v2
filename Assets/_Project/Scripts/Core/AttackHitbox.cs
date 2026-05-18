using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(DamageDealer))]
public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private Health _ownerHealth;

    private Collider2D _collider;
    private DamageDealer _damageDealer;
    private bool _isInitialized;

    private void Awake()
    {
        Initialize();
        Disable();
    }

    public void Enable()
    {
        Initialize();
        _collider.enabled = true;
    }

    public void Disable()
    {
        Initialize();
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Initialize();

        if (other.TryGetComponent(out Health targetHealth) == false)
            return;

        if (targetHealth == _ownerHealth)
            return;

        targetHealth.TakeDamage(_damageDealer.Damage);
    }

    private void Initialize()
    {
        if (_isInitialized)
            return;

        _collider = GetComponent<Collider2D>();
        _damageDealer = GetComponent<DamageDealer>();

        _collider.isTrigger = true;
        _isInitialized = true;
    }
}