using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerBase<T> : MonoBehaviour where T : SpawnableObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private bool _spawnOnStart = true;

    private readonly List<T> _spawnedObjects = new();

    protected IReadOnlyList<T> SpawnedObjects => _spawnedObjects;
    protected T Prefab => _prefab;

    private void Start()
    {
        if (_spawnOnStart)
            SpawnAll();
    }

    public virtual void SpawnAll()
    {
        Clear();

        if (_prefab == null)
            return;

        foreach (Transform spawnPoint in _spawnPoints)
        {
            if (spawnPoint == null)
                continue;

            Spawn(spawnPoint);
        }
    }

    protected T Spawn(Transform spawnPoint)
    {
        T spawnedObject = Instantiate(
            _prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        _spawnedObjects.Add(spawnedObject);
        OnSpawned(spawnedObject, spawnPoint);

        return spawnedObject;
    }

    public virtual void Clear()
    {
        for (int i = _spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (_spawnedObjects[i] != null)
                Destroy(_spawnedObjects[i].gameObject);
        }

        _spawnedObjects.Clear();
    }

    protected virtual void OnSpawned(T spawnedObject, Transform spawnPoint)
    {
    }

    protected void RemoveFromSpawned(T spawnedObject)
    {
        _spawnedObjects.Remove(spawnedObject);
    }

    private void OnDestroy()
    {
        Clear();
    }
}