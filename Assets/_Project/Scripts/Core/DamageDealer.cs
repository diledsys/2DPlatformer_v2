using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    public int Damage => _damage;
}