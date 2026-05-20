using UnityEngine;

public class BrokenEnemyRagdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] _parts;
    [SerializeField] private float _force = 2.5f;
    [SerializeField] private float _upForce = 2f;
    [SerializeField] private float _torque = 4f;
    [SerializeField] private float _destroyDelay = 3f;

    private void Awake()
    {
        if (_parts == null || _parts.Length == 0)
            _parts = GetComponentsInChildren<Rigidbody2D>();
    }

    public void Explode()
    {
        foreach (Rigidbody2D part in _parts)
        {
            if (part == null)
                continue;

            part.bodyType = RigidbodyType2D.Dynamic;
            part.gravityScale = 1f;
            part.simulated = true;

            Vector2 direction = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(0.2f, 1f)
            ).normalized;

            Vector2 force = new Vector2(
                direction.x * _force,
                direction.y * _upForce
            );

            part.linearVelocity = Vector2.zero;
            part.angularVelocity = 0f;

            part.AddForce(force, ForceMode2D.Impulse);
            part.AddTorque(Random.Range(-_torque, _torque), ForceMode2D.Impulse);
        }

        Destroy(gameObject, _destroyDelay);
    }
}