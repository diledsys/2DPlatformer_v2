using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private CollectableItem _prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private bool _spawnOnStart = true;
    [SerializeField] private bool _respawnAfterCollect = true;
    [SerializeField] private float _respawnDelay = 5f;

    private readonly List<CollectableItem> _spawnedItems = new();
    private readonly Dictionary<CollectableItem, Transform> _spawnPointsByItem = new();

    private void Start()
    {
        if (_spawnOnStart)
            SpawnAll();
    }

    private void OnDestroy()
    {
        Clear();
    }

    public void SpawnAll()
    {
        Clear();

        foreach (Transform spawnPoint in _spawnPoints)
        {
            if (spawnPoint == null)
                continue;

            Spawn(spawnPoint);
        }
    }

    public void Clear()
    {
        foreach (CollectableItem item in _spawnPointsByItem.Keys)
        {
            if (item != null)
                item.Collected -= OnCollected;
        }

        _spawnPointsByItem.Clear();

        for (int i = _spawnedItems.Count - 1; i >= 0; i--)
        {
            if (_spawnedItems[i] != null)
                Destroy(_spawnedItems[i].gameObject);
        }

        _spawnedItems.Clear();
    }

    private void Spawn(Transform spawnPoint)
    {
        if (_prefab == null)
            return;

        CollectableItem item = Instantiate(
            _prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        _spawnedItems.Add(item);
        _spawnPointsByItem[item] = spawnPoint;

        item.Collected += OnCollected;
    }

    private void OnCollected(CollectableItem item)
    {
        if (_spawnPointsByItem.TryGetValue(item, out Transform spawnPoint) == false)
            return;

        item.Collected -= OnCollected;
        _spawnPointsByItem.Remove(item);
        _spawnedItems.Remove(item);

        Destroy(item.gameObject);

        if (_respawnAfterCollect)
            StartCoroutine(RespawnAfterDelay(spawnPoint));
    }

    private IEnumerator RespawnAfterDelay(Transform spawnPoint)
    {
        yield return new WaitForSeconds(_respawnDelay);

        if (spawnPoint != null)
            Spawn(spawnPoint);
    }
}