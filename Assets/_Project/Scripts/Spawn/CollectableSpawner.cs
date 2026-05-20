using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : SpawnerBase<CollectableSpawnable>
{
    [SerializeField] private bool _respawnAfterCollect = true;
    [SerializeField] private float _respawnDelay = 5f;

    private readonly Dictionary<CollectableItem, Transform> _spawnPointsByCollectable = new();

    protected override void OnSpawned(CollectableSpawnable spawnedObject, Transform spawnPoint)
    {
        if (spawnedObject.TryGetComponent(out CollectableItem collectable) == false)
            return;

        _spawnPointsByCollectable[collectable] = spawnPoint;
        collectable.Collected += OnCollected;
    }

    private void OnCollected(CollectableItem collectable)
    {
        if (_spawnPointsByCollectable.TryGetValue(collectable, out Transform spawnPoint) == false)
            return;

        collectable.Collected -= OnCollected;
        _spawnPointsByCollectable.Remove(collectable);

        if (collectable.TryGetComponent(out CollectableSpawnable spawnable))
            RemoveFromSpawned(spawnable);

        Destroy(collectable.gameObject);

        if (_respawnAfterCollect)
            StartCoroutine(RespawnAfterDelay(spawnPoint));
    }

    private IEnumerator RespawnAfterDelay(Transform spawnPoint)
    {
        yield return new WaitForSeconds(_respawnDelay);

        if (spawnPoint == null)
            yield break;

        Spawn(spawnPoint);
    }

    public override void Clear()
    {
        foreach (CollectableItem collectable in _spawnPointsByCollectable.Keys)
        {
            if (collectable != null)
                collectable.Collected -= OnCollected;
        }

        _spawnPointsByCollectable.Clear();
        base.Clear();
    }
}