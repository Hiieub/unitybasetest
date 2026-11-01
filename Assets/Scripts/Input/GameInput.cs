using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInput;

    public event Action OnJump;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();

        playerInput.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke();
    }

    private void OnDestroy()
    {
        playerInput.Player.Disable();
        playerInput.Dispose();

        playerInput.Player.Jump.performed -= Jump_performed;
    }

    public Vector2 GetMovement()
    {
        Vector2 moveInput = playerInput.Player.Move.ReadValue<Vector2>();

        return moveInput.normalized;
    }
}
