using UnityEngine;

public class BrokenRagdoll2D : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField] private RagdollPart2D[] _parts;

    [Header("Physics")]
    [SerializeField] private float _sideForce = 2.5f;
    [SerializeField] private float _upForce = 2f;
    [SerializeField] private float _torque = 5f;

    [Header("Lifetime")]
    [SerializeField] private float _destroyDelay = 3f;

    private bool _isActivated;

    private void Awake()
    {
        if (_parts == null || _parts.Length == 0)
            _parts = GetComponentsInChildren<RagdollPart2D>(true);
    }

    public void Activate()
    {
        if (_isActivated)
            return;

        _isActivated = true;

        foreach (RagdollPart2D part in _parts)
        {
            if (part == null)
                continue;

            Vector2 direction = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(0.3f, 1f)
            ).normalized;

            Vector2 force = new Vector2(
                direction.x * _sideForce,
                direction.y * _upForce
            );

            float randomTorque = Random.Range(-_torque, _torque);

            part.EnablePhysics(force, randomTorque);
        }

        Destroy(gameObject, _destroyDelay);
    }
}