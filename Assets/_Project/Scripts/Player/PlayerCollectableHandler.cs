using UnityEngine;

[RequireComponent(typeof(PlayerCollector))]
[RequireComponent(typeof(PlayerScore))]
[RequireComponent(typeof(Health))]
public class PlayerCollectableHandler : MonoBehaviour
{
    private PlayerCollector _collector;
    private PlayerScore _score;
    private Health _health;

    private void Awake()
    {
        _collector = GetComponent<PlayerCollector>();
        _score = GetComponent<PlayerScore>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _collector.CollectableDetected += OnCollectableDetected;
    }

    private void OnDisable()
    {
        _collector.CollectableDetected -= OnCollectableDetected;
    }

    private void OnCollectableDetected(CollectableItem collectable)
    {
        bool wasApplied = false;

        if (collectable is IScoreReward scoreReward)
        {
            _score.Add(scoreReward.ScoreValue);
            wasApplied = true;
        }

        if (collectable is IHealthReward healthReward)
        {
            if (_health.CurrentValue < _health.MaxValue)
            {
                _health.Heal(healthReward.HealValue);
                wasApplied = true;
            }
        }

        if (wasApplied)
            collectable.Collect();
    }
}