using UnityEngine;

[RequireComponent(typeof(Health))]
public class CharacterDeathHandler : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _mainCollider;

    [Header("Movement")]
    [SerializeField] private Mover2D _mover;

    [Header("Disable On Death")]
    [SerializeField] private MonoBehaviour[] _componentsToDisable;

    private Health _health;
    private bool _isDead;

    private void Awake()
    {
        _health = GetComponent<Health>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_mainCollider == null)
            _mainCollider = GetComponent<Collider2D>();

        if (_mover == null)
            _mover = GetComponent<Mover2D>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        if (_isDead)
            return;

        _isDead = true;

        StopMovement();
        DisableMainPhysics();
        DisableGameplayComponents();
    }

    private void StopMovement()
    {
        if (_mover != null)
            _mover.Stop();

        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
        }
    }

    private void DisableMainPhysics()
    {
        if (_mainCollider != null)
            _mainCollider.enabled = false;

        if (_rigidbody != null)
            _rigidbody.simulated = false;
    }

    private void DisableGameplayComponents()
    {
        foreach (MonoBehaviour component in _componentsToDisable)
        {
            if (component != null)
                component.enabled = false;
        }
    }
}