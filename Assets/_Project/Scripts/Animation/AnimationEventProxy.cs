using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private MeleeAttack _attack;

    private void Awake()
    {
        if (_attack == null)
            _attack = GetComponentInParent<MeleeAttack>();
    }

    public void OpenHitbox()
    {
        _attack.OpenHitbox();
    }

    public void CloseHitbox()
    {
        _attack.CloseHitbox();
    }

    public void FinishAttack()
    {
        _attack.FinishAttack();
    }
}