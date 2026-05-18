using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Health))]
public class EnemyRagdollDeath : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _visualRoot;
    [SerializeField] private Animator _animator;

    [Header("Physics")]
    [SerializeField] private float _sideForce = 2.5f;
    [SerializeField] private float _upForce = 2f;
    [SerializeField] private float _torque = 5f;

    [Header("Destroy")]
    [SerializeField] private float _destroyDelay = 3f;

    [Header("Options")]
    [SerializeField] private bool _detachPartsFromParent = true;
    [SerializeField] private bool _disableSpriteSkin = true;

    private Health _health;
    private Rigidbody2D[] _parts;
    private bool _isActivated;

    private void Awake()
    {
        _health = GetComponent<Health>();

        if (_visualRoot == null)
            _visualRoot = transform;

        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();

        _parts = _visualRoot.GetComponentsInChildren<Rigidbody2D>(true);
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        if (_isActivated)
            return;

        _isActivated = true;

        DisableRigControllers();
        ActivateRagdoll();

        Destroy(gameObject, _destroyDelay);
    }

    private void DisableRigControllers()
    {
        if (_animator != null)
            _animator.enabled = false;

        Behaviour[] behaviours = _visualRoot.GetComponentsInChildren<Behaviour>(true);

        foreach (Behaviour behaviour in behaviours)
        {
            if (behaviour == null)
                continue;

            string typeName = behaviour.GetType().Name;

            if (typeName.Contains("IK") || typeName.Contains("Solver"))
                behaviour.enabled = false;
        }

        if (_disableSpriteSkin)
        {
            SpriteSkin[] spriteSkins = _visualRoot.GetComponentsInChildren<SpriteSkin>(true);

            foreach (SpriteSkin spriteSkin in spriteSkins)
            {
                spriteSkin.enabled = false;
            }
        }
    }

    private void ActivateRagdoll()
    {
        Debug.Log($"{name}: ragdoll rigidbodies = {_parts.Length}");

        foreach (Rigidbody2D part in _parts)
        {
            if (part == null)
                continue;

            if (part.transform == transform)
                continue;

            EnablePartPhysics(part);
        }
    }

    private void EnablePartPhysics(Rigidbody2D part)
    {
        if (_detachPartsFromParent)
            part.transform.SetParent(null, true);

        foreach (Collider2D collider in part.GetComponents<Collider2D>())
        {
            collider.enabled = true;
            collider.isTrigger = false;
        }

        part.simulated = true;
        part.bodyType = RigidbodyType2D.Dynamic;
        part.gravityScale = 1f;
        part.linearVelocity = Vector2.zero;
        part.angularVelocity = 0f;
        part.constraints = RigidbodyConstraints2D.None;

        Vector2 direction = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(0.3f, 1f)
        ).normalized;

        Vector2 force = new Vector2(
            direction.x * _sideForce,
            direction.y * _upForce
        );

        float randomTorque = Random.Range(-_torque, _torque);

        part.AddForce(force, ForceMode2D.Impulse);
        part.AddTorque(randomTorque, ForceMode2D.Impulse);
    }
}