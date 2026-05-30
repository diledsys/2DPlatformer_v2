using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private Character _character;

    public void OpenHitbox()
    {
        _character.OpenAttackHitbox();
    }

    public void CloseHitbox()
    {
        _character.CloseAttackHitbox();
    }

    public void FinishAttack()
    {
        _character.FinishAttack();
    }
}