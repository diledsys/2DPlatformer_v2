using TMPro;
using UnityEngine;

public sealed class HealthTextView : HealthView
{
    [SerializeField] private TMP_Text _healthText;

    protected override void Display(int currentValue, int maxValue)
    {
        _healthText.text = $"{currentValue}/{maxValue}";
    }
}
