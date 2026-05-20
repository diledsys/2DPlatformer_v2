using UnityEngine;

public class HeartCollectable : CollectableItem,IHealthReward
{
    [SerializeField] private int _healValue = 1;

    public int HealValue => _healValue;
}