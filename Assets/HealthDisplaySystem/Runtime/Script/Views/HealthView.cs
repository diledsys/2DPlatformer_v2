using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;

    private bool _isStarted;

    protected virtual void OnEnable()
    {
        _health.Changed += OnHealthChanged;

        if (_isStarted)
            Refresh();
    }

    protected virtual void Start()
    {
        _isStarted = true;
        Refresh();
    }

    protected virtual void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
    }

    protected abstract void Display(int currentValue, int maxValue);

    private void OnHealthChanged(int currentValue, int maxValue)
    {
        Display(currentValue, maxValue);
    }

    private void Refresh()
    {
        Display(_health.CurrentValue, _health.MaxValue);
    }
}
