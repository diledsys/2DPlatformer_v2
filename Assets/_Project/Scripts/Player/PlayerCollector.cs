using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerScore))]
public class PlayerCollector : MonoBehaviour
{
    public Health Health { get; private set; }
    public PlayerScore Score { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        Score = GetComponent<PlayerScore>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectable collectable) == false)
            return;

        collectable.Collect(this);
    }
}