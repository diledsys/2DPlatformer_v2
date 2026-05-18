using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private CharacterAttack _attack;

    private void Awake()
    {
        if (_attack == null)
            _attack = GetComponentInParent<CharacterAttack>();
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