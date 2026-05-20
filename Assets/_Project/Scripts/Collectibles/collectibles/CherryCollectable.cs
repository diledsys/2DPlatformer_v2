using UnityEngine;

public class CherryCollectable : CollectableItem, IScoreReward
{
    [SerializeField] private int _scoreValue = 1;

    public int ScoreValue => _scoreValue;
}