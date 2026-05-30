using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public event Action<CollectableItem> CollectableDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CollectableItem collectable) == false)
            return;

        CollectableDetected?.Invoke(collectable);
    }
}