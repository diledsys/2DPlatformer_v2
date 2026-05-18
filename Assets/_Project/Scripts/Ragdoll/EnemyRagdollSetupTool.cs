using UnityEngine;

public class EnemyRagdollSetupTool : MonoBehaviour
{
    [Header("Root")]
    [SerializeField] private Transform _root;
    [SerializeField] private bool _includeInactive = true;

    [Header("Filters")]
    [SerializeField] private bool _addOnlyToSpriteRenderers = true;
    [SerializeField] private float _minSpriteSize = 0.05f;
    [SerializeField]
    private string[] _ignoredNameParts =
    {
        "Hitbox",
        "Sensor",
        "GroundCheck",
        "CameraTarget",
        "VisualPivot"
    };

    [Header("Collider")]
    [SerializeField] private float _colliderSizeMultiplier = 0.8f;

    [ContextMenu("Setup Ragdoll Parts")]
    private void SetupRagdollParts()
    {
        if (_root == null)
            _root = transform;

        SpriteRenderer[] renderers = _root.GetComponentsInChildren<SpriteRenderer>(_includeInactive);
        int addedCount = 0;

        foreach (SpriteRenderer renderer in renderers)
        {
            GameObject target = renderer.gameObject;

            if (ShouldIgnore(target, renderer))
                continue;

            AddRigidbody(target);
            AddCollider(target, renderer);
            AddEnemyBonePart(target);

            addedCount++;
        }

        Debug.Log($"{name}: ragdoll setup completed. Parts configured: {addedCount}");
    }

    [ContextMenu("Remove Ragdoll Parts")]
    private void RemoveRagdollParts()
    {
        if (_root == null)
            _root = transform;

        EnemyBonePart[] parts = _root.GetComponentsInChildren<EnemyBonePart>(_includeInactive);

        foreach (EnemyBonePart part in parts)
        {
            GameObject target = part.gameObject;

            DestroyImmediate(part);

            if (target.TryGetComponent(out Collider2D collider))
                DestroyImmediate(collider);

            if (target.TryGetComponent(out Rigidbody2D rigidbody))
                DestroyImmediate(rigidbody);
        }

        Debug.Log($"{name}: ragdoll parts removed");
    }

    private bool ShouldIgnore(GameObject target, SpriteRenderer renderer)
    {
        foreach (string ignoredNamePart in _ignoredNameParts)
        {
            if (string.IsNullOrWhiteSpace(ignoredNamePart))
                continue;

            if (target.name.Contains(ignoredNamePart))
                return true;
        }

        if (_addOnlyToSpriteRenderers && renderer == null)
            return true;

        Vector2 size = renderer.bounds.size;

        if (size.x < _minSpriteSize || size.y < _minSpriteSize)
            return true;

        return false;
    }

    private void AddRigidbody(GameObject target)
    {
        if (target.TryGetComponent(out Rigidbody2D rigidbody) == false)
            rigidbody = target.AddComponent<Rigidbody2D>();

        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.gravityScale = 1f;
        rigidbody.simulated = false;
        rigidbody.freezeRotation = false;
    }

    private void AddCollider(GameObject target, SpriteRenderer renderer)
    {
        if (target.TryGetComponent(out Collider2D _))
            return;

        BoxCollider2D collider = target.AddComponent<BoxCollider2D>();
        collider.isTrigger = false;
        collider.enabled = false;

        Vector2 size = renderer.bounds.size;
        Vector3 localSize = target.transform.InverseTransformVector(size);

        collider.size = new Vector2(
            Mathf.Abs(localSize.x) * _colliderSizeMultiplier,
            Mathf.Abs(localSize.y) * _colliderSizeMultiplier
        );
    }

    private void AddEnemyBonePart(GameObject target)
    {
        if (target.TryGetComponent(out EnemyBonePart _))
            return;

        target.AddComponent<EnemyBonePart>();
    }
}