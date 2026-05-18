using UnityEngine;

[RequireComponent(typeof(Mover2D))]
public class EnemyPatrolController : MonoBehaviour
{
    [SerializeField] private float _changeDirectionDelay = 2f;

    private Mover2D _mover;
    private float _direction = 1f;
    private float _timer;

    private void Awake()
    {
        _mover = GetComponent<Mover2D>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _changeDirectionDelay)
        {
            _timer = 0f;
            _direction *= -1f;
        }

        _mover.SetDirection(_direction);
    }
}