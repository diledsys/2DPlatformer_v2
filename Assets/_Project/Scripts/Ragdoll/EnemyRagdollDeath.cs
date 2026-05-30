using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Health))]
public class EnemyRagdollDeath : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _visualRoot;
    [SerializeField] private Animator _animator;

    [Header("Ragdoll Parts")]
    [SerializeField] private Rigidbody2D[] _parts;

    [Header("Rig Controllers To Disable")]
    [SerializeField] private Behaviour[] _rigControllersToDisable;
    [SerializeField] private SpriteSkin[] _spriteSkinsToDisable;

    [Header("Physics")]
    [SerializeField] private float _sideForce = 2.5f;
    [SerializeField] private float _upForce = 2f;
    [SerializeField] private float _torque = 5f;

    [Header("Destroy")]
    [SerializeField] private float _destroyDelay = 3f;

    [Header("Options")]
    [SerializeField] private bool _detachPartsFromParent = true;

    private readonly List<GameObject> _detachedParts = new();

    private Health _health;
    private bool _isActivated;

    private void Awake()
    {
        _health = GetComponent<Health>();
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

        StartCoroutine(DestroyAfterDelay());
    }

    private void DisableRigControllers()
    {
        if (_animator != null)
            _animator.enabled = false;

        foreach (Behaviour behaviour in _rigControllersToDisable)
        {
            if (behaviour != null)
                behaviour.enabled = false;
        }

        foreach (SpriteSkin spriteSkin in _spriteSkinsToDisable)
        {
            if (spriteSkin != null)
                spriteSkin.enabled = false;
        }
    }

    private void ActivateRagdoll()
    {
        if (_parts == null || _parts.Length == 0)
            return;


        foreach (Rigidbody2D part in _parts)
        {
            if (part == null)
                continue;

            EnablePartPhysics(part);
        }
    }

    private void EnablePartPhysics(Rigidbody2D part)
    {
        if (_detachPartsFromParent)
        {
            part.transform.SetParent(null, true);
            _detachedParts.Add(part.gameObject);
        }

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

        part.AddForce(force, ForceMode2D.Impulse);
        part.AddTorque(Random.Range(-_torque, _torque), ForceMode2D.Impulse);
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(_destroyDelay);

        foreach (GameObject part in _detachedParts)
        {
            if (part != null)
                Destroy(part);
        }

        Destroy(gameObject);
    }

#if UNITY_EDITOR
    [ContextMenu("Collect Ragdoll Parts")]
    private void CollectRagdollParts()
    {
        Transform searchRoot = _visualRoot != null ? _visualRoot : transform;
        Rigidbody2D rootRigidbody = GetComponent<Rigidbody2D>();

        Rigidbody2D[] foundRigidbodies = searchRoot.GetComponentsInChildren<Rigidbody2D>(true);

        List<Rigidbody2D> validParts = new();

        foreach (Rigidbody2D rigidbody in foundRigidbodies)
        {
            if (rigidbody == null)
                continue;

            if (rigidbody == rootRigidbody)
                continue;

            if (ShouldIgnore(rigidbody.gameObject))
                continue;

            validParts.Add(rigidbody);
        }

        _parts = validParts.ToArray();

        UnityEditor.EditorUtility.SetDirty(this);
    }

    private bool ShouldIgnore(GameObject target)
    {
        string objectName = target.name;

        return objectName.Contains("Hitbox") ||
               objectName.Contains("Sensor") ||
               objectName.Contains("GroundCheck") ||
               objectName.Contains("CameraTarget") ||
               objectName.Contains("Attack") ||
               objectName.Contains("Bite") ||
               objectName.Contains("Sword");
    }
#endif
}