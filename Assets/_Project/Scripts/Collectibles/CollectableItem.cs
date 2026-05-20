using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class CollectableItem : SpawnableObject
{
    private bool _isCollected;

    public event Action<CollectableItem> Collected;

    protected virtual void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    public void Collect()
    {
        if (_isCollected)
            return;

        _isCollected = true;
        Collected?.Invoke(this);
    }
}