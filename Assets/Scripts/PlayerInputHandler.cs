using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] PlayerCharacter _playerCharacter;
    [SerializeField] Interactor _interactor;

    InputAction _moveAction;
    InputAction _interactAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction = _playerInput.actions["Move"];
        _interactAction = _playerInput.actions["Interact"];
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Get movement input from the Player Input
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();

        // Calculate movement direction (converting 2D input to 3D world space)
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        // Move the player character
        _playerCharacter.Move(moveDirection);

        // Check for interaction input (e.g., pressing the "Interact" button)
        if (_interactAction.WasPressedThisFrame())
        {
            _interactor.Interact();
        }
    }
}
