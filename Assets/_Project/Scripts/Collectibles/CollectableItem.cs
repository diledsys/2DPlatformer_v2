using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectableItem : MonoBehaviour, ICollectable
{
    private enum CollectableType
    {
        Score,
        Heal
    }

    [SerializeField] private CollectableType _type = CollectableType.Score;
    [SerializeField] private int _value = 1;

    private bool _isCollected;

    private void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    public void Collect(PlayerCollector collector)
    {
        if (_isCollected)
            return;

        if (collector == null)
            return;

        _isCollected = true;

        ApplyEffect(collector);
        Destroy(gameObject);
    }

    private void ApplyEffect(PlayerCollector collector)
    {
        switch (_type)
        {
            case CollectableType.Score:
                collector.Score.Add(_value);
                break;

            case CollectableType.Heal:
                collector.Health.Heal(_value);
                break;
        }
    }
}