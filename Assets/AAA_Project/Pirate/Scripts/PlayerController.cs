using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInput Input => _playerInput;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}