using UnityEngine;
using UnityEngine.UI;

public sealed class HealthDemoInput : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Button _damageButton;
    [SerializeField] private Button _healButton;
    [SerializeField, Min(1)] private int _damageValue = 1;
    [SerializeField, Min(1)] private int _healValue = 1;

    private void OnEnable()
    {
        _damageButton.onClick.AddListener(ApplyDamage);
        _healButton.onClick.AddListener(ApplyHealing);
    }

    private void OnDisable()
    {
        _damageButton.onClick.RemoveListener(ApplyDamage);
        _healButton.onClick.RemoveListener(ApplyHealing);
    }

    private void ApplyDamage()
    {
        _health.TakeDamage(_damageValue);
    }

    private void ApplyHealing()
    {
        _health.TakeHealing(_healValue);
    }
}
