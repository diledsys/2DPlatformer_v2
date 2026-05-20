using UnityEngine;

public class PlayerSpawner : SpawnerBase<PlayerSpawnable>
{
    [SerializeField] private CameraBinder _cameraBinder;

    protected override void OnSpawned(PlayerSpawnable spawnedObject, Transform spawnPoint)
    {
        if (_cameraBinder == null)
            return;

        if (spawnedObject.TryGetComponent(out PlayerCameraTarget cameraTarget))
            _cameraBinder.Bind(cameraTarget);
    }
}