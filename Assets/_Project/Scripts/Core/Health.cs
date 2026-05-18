using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 3;

    private int _currentValue;

    public int CurrentValue => _currentValue;
    public int MaxValue => _maxValue;
    public bool IsDead => _currentValue <= 0;

    public event Action<int, int> Changed;
    public event Action Damaged;
    public event Action Died;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        if (IsDead)
            return;

        _currentValue = Mathf.Max(_currentValue - damage, 0);

        Changed?.Invoke(_currentValue, _maxValue);
        Damaged?.Invoke();

        if (IsDead)
            Died?.Invoke();
    }

    public void Heal(int value)
    {
        if (value <= 0)
            return;

        if (IsDead)
            return;

        _currentValue = Mathf.Min(_currentValue + value, _maxValue);
        Changed?.Invoke(_currentValue, _maxValue);

        Debug.Log($"{name} healed: {_currentValue}/{_maxValue}");
    }
}