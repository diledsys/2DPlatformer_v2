using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public event Action<CollectableItem> CollectableDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CollectableItem collectable = other.GetComponentInParent<CollectableItem>();

        if (collectable == null)
            return;

        CollectableDetected?.Invoke(collectable);
    }
}