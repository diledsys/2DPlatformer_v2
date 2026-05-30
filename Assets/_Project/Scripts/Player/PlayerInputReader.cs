using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInputSystem _input;

    public event Action<float> MoveChanged;
    public event Action<bool> SprintChanged;
    public event Action JumpPressed;
    public event Action AttackPressed;

    private void Awake()
    {
        _input = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;

        _input.Player.Jump.performed += OnJumpPerformed;

        _input.Player.Sprint.performed += OnSprintPerformed;
        _input.Player.Sprint.canceled += OnSprintCanceled;

        _input.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;

        _input.Player.Jump.performed -= OnJumpPerformed;

        _input.Player.Sprint.performed -= OnSprintPerformed;
        _input.Player.Sprint.canceled -= OnSprintCanceled;

        _input.Player.Attack.performed -= OnAttackPerformed;

        _input.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        MoveChanged?.Invoke(input.x);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveChanged?.Invoke(0f);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpPressed?.Invoke();
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        SprintChanged?.Invoke(true);
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        SprintChanged?.Invoke(false);
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackPressed?.Invoke();
    }
}