public sealed class HealthBarView : HealthSliderView
{
    protected override void DisplayNormalized(float normalizedValue)
    {
        SliderValue = normalizedValue;
    }
}
