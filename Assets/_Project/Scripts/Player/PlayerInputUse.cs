using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(Mover2D))]
[RequireComponent(typeof(Jumper2D))]
[RequireComponent(typeof(Character))]
public class PlayerInputUse : MonoBehaviour
{
    private PlayerInputReader _input;
    private Mover2D _mover;
    private Jumper2D _jumper;
    private Character _character;

    private void Awake()
    {
        _input = GetComponent<PlayerInputReader>();
        _mover = GetComponent<Mover2D>();
        _jumper = GetComponent<Jumper2D>();
        _character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        _input.MoveChanged += OnMoveChanged;
        _input.SprintChanged += OnSprintChanged;
        _input.JumpPressed += OnJumpPressed;
        _input.AttackPressed += OnAttackPressed;
    }

    private void OnDisable()
    {
        _input.MoveChanged -= OnMoveChanged;
        _input.SprintChanged -= OnSprintChanged;
        _input.JumpPressed -= OnJumpPressed;
        _input.AttackPressed -= OnAttackPressed;
    }

    private void OnMoveChanged(float direction)
    {
        _mover.SetDirection(direction);
    }

    private void OnSprintChanged(bool isSprinting)
    {
        _mover.SetRunning(isSprinting);
    }

    private void OnJumpPressed()
    {
        _jumper.Jump();
    }

    private void OnAttackPressed()
    {
        _character.Attack();
    }
}