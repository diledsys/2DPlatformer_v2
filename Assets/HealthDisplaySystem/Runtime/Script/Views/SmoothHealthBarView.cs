using UnityEngine;

public sealed class SmoothHealthBarView : HealthSliderView
{
    [SerializeField, Min(0.01f)] private float _changeSpeed = 0.5f;

    private float _targetValue;
    private bool _isInitialized;

    protected override void OnEnable()
    {
        _isInitialized = false;
        base.OnEnable();
    }

    private void Update()
    {
        if (Mathf.Approximately(SliderValue, _targetValue))
            return;

        SliderValue = Mathf.MoveTowards(
            SliderValue,
            _targetValue,
            _changeSpeed * Time.deltaTime);
    }

    protected override void DisplayNormalized(float normalizedValue)
    {
        _targetValue = normalizedValue;

        if (_isInitialized)
            return;

        SliderValue = _targetValue;
        _isInitialized = true;
    }
}
