using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthDebugView : MonoBehaviour
{
    [SerializeField] private string _label = "Character";

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
        _health.Died += OnDied;
    }

    private void Start()
    {
        Debug.Log($"{_label} health: {_health.CurrentValue}/{_health.MaxValue}");
    }

    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
        _health.Died -= OnDied;
    }

    private void OnHealthChanged(int currentValue, int maxValue)
    {
        Debug.Log($"{_label} health: {currentValue}/{maxValue}");
    }

    private void OnDied()
    {
        Debug.Log($"{_label} died");
    }
}