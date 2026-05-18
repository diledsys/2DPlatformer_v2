using UnityEngine;

public class PlayerCameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public Transform Target => _target;

    private void Awake()
    {
        if (_target == null)
            _target = transform;
    }
}