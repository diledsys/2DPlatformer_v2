using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Character _prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private bool _spawnOnStart = true;

    private readonly List<Character> _spawnedEnemies = new();

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
        for (int i = _spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (_spawnedEnemies[i] != null)
                Destroy(_spawnedEnemies[i].gameObject);
        }

        _spawnedEnemies.Clear();
    }

    private void Spawn(Transform spawnPoint)
    {
        if (_prefab == null)
            return;

        Character enemy = Instantiate(
            _prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        _spawnedEnemies.Add(enemy);
    }
}