using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 3;

    private int _currentValue;

    public event Action<int, int> Changed;
    public event Action Damaged;
    public event Action Died;

    public int CurrentValue => _currentValue;
    public int MaxValue => _maxValue;
    public bool IsDead => _currentValue <= 0;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        int previousValue = _currentValue;

        _currentValue = Mathf.Max(_currentValue - damage, 0);

        if (_currentValue == previousValue)
            return;

        Changed?.Invoke(_currentValue, _maxValue);
        Damaged?.Invoke();

        if (previousValue > 0 && IsDead)
            Died?.Invoke();
    }

    public void TakeHealing(int value)
    {
        if (value <= 0)
            return;

        int previousValue = _currentValue;

        _currentValue = Mathf.Min(_currentValue + value, _maxValue);

        if (_currentValue == previousValue)
            return;

        Changed?.Invoke(_currentValue, _maxValue);
    }
}
