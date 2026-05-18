using Unity.Cinemachine;
using UnityEngine;

public class CameraBinder : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cinemachineCamera;

    public void Bind(PlayerCameraTarget playerCameraTarget)
    {
        if (playerCameraTarget == null)
            return;

        _cinemachineCamera.Target.TrackingTarget = playerCameraTarget.Target;
    }
}