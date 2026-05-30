using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Character _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CameraBinder _cameraTargetBinder;
    [SerializeField] private bool _spawnOnStart = true;

    private Character _spawnedPlayer;

    private void Start()
    {
        if (_spawnOnStart)
            Spawn();
    }

    private void OnDestroy()
    {
        Clear();
    }

    public Character Spawn()
    {
        Clear();

        if (_prefab == null || _spawnPoint == null)
            return null;

        _spawnedPlayer = Instantiate(
            _prefab,
            _spawnPoint.position,
            _spawnPoint.rotation
        );

        if (_cameraTargetBinder != null &&
            _spawnedPlayer.TryGetComponent(out PlayerCameraTarget cameraTarget))
        {
            _cameraTargetBinder.Bind(cameraTarget);
        }

        return _spawnedPlayer;
    }

    public void Clear()
    {
        if (_spawnedPlayer != null)
            Destroy(_spawnedPlayer.gameObject);

        _spawnedPlayer = null;
    }
}