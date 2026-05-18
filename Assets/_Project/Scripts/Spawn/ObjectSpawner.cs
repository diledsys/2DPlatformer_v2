using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Serializable]
    private class SpawnEntry
    {
        [SerializeField] private SpawnableObject _prefab;
        [SerializeField] private Transform[] _spawnPoints;

        public SpawnableObject Prefab => _prefab;
        public Transform[] SpawnPoints => _spawnPoints;
    }

    [SerializeField] private SpawnEntry[] _spawnEntries;
    [SerializeField] private bool _spawnOnStart = true;
    [SerializeField] private CameraBinder _cameraBinder;

    private readonly List<SpawnableObject> _spawnedObjects = new();

    private void Start()
    {
        if (_spawnOnStart)
            SpawnAll();
    }

    public void SpawnAll()
    {
        ClearSpawnedObjects();

        foreach (SpawnEntry entry in _spawnEntries)
        {
            if (entry.Prefab == null)
                continue;

            foreach (Transform spawnPoint in entry.SpawnPoints)
            {
                if (spawnPoint == null)
                    continue;

                SpawnableObject spawnedObject = Spawn(entry.Prefab, spawnPoint);
                BindCameraIfPlayer(spawnedObject);
            }
        }
    }

    private SpawnableObject Spawn(SpawnableObject prefab, Transform spawnPoint)
    {
        SpawnableObject spawnedObject = Instantiate(
            prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        _spawnedObjects.Add(spawnedObject);

        return spawnedObject;
    }

    private void BindCameraIfPlayer(SpawnableObject spawnedObject)
    {
        if (_cameraBinder == null)
            return;

        if (spawnedObject.TryGetComponent(out PlayerCameraTarget playerCameraTarget))
            _cameraBinder.Bind(playerCameraTarget);
    }

    public void ClearSpawnedObjects()
    {
        for (int i = _spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (_spawnedObjects[i] != null)
                Destroy(_spawnedObjects[i].gameObject);
        }

        _spawnedObjects.Clear();
    }

    private void OnDestroy()
    {
        ClearSpawnedObjects();
    }
}