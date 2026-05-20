using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttackTrigger : MonoBehaviour
{
    [SerializeField] private MeleeAttack _attack;

    private void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;

        if (_attack == null)
            _attack = GetComponentInParent<MeleeAttack>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInputReader _) == false)
            return;

        _attack.Attack();
    }
}