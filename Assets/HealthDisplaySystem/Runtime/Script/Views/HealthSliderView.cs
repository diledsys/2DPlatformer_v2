using UnityEngine;
using UnityEngine.UI;

public abstract class HealthSliderView : HealthView
{
    [SerializeField] private Slider _slider;

    protected float SliderValue
    {
        get => _slider.value;
        set => _slider.value = value;
    }

    protected virtual void Awake()
    {
        _slider.minValue = 0f;
        _slider.maxValue = 1f;
        _slider.wholeNumbers = false;
        _slider.interactable = false;
    }

    protected sealed override void Display(int currentValue, int maxValue)
    {
        DisplayNormalized(GetNormalizedValue(currentValue, maxValue));
    }

    protected abstract void DisplayNormalized(float normalizedValue);

    private static float GetNormalizedValue(int currentValue, int maxValue)
    {
        if (maxValue <= 0)
            return 0f;

        return Mathf.Clamp01((float)currentValue / maxValue);
    }
}
