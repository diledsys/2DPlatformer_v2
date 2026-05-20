using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover2D))]
[RequireComponent(typeof(Jumper2D))]
public class PlayerInputReader : MonoBehaviour
{
    private PlayerInputSystem _input;
    private Mover2D _mover;
    private Jumper2D _jumper;
    private MeleeAttack _attack;

    private Vector2 _moveInput;
    private bool _isRunning;

    private void Awake()
    {
        _input = new PlayerInputSystem();

        _mover = GetComponent<Mover2D>();
        _jumper = GetComponent<Jumper2D>();
        _attack = GetComponent<MeleeAttack>();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;

        _input.Player.Jump.performed += OnJumpPerformed;

        _input.Player.Sprint.performed += OnRunPerformed;
        _input.Player.Sprint.canceled += OnRunCanceled;

        _input.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;

        _input.Player.Jump.performed -= OnJumpPerformed;

        _input.Player.Sprint.performed -= OnRunPerformed;
        _input.Player.Sprint.canceled -= OnRunCanceled;

        _input.Player.Attack.performed -= OnAttackPerformed;

        _input.Disable();
    }

    private void Update()
    {
        _mover.SetDirection(_moveInput.x);
        _mover.SetRunning(_isRunning);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _jumper.Jump();
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        _isRunning = true;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        _isRunning = false;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (_attack == null)
            return;

        _attack.Attack();
    }
}