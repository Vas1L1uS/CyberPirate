using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _speed;

    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = _playerController.Input;
    }


    private void Update()
    {
        if (_playerController.Health.CurrentHealth == 0) return;

        Move();
    }

    private void Move()
    {
        Vector2 direction = _playerInput.Player.Movement.ReadValue<Vector2>();

        if (direction == Vector2.zero) return;

        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(direction.x, 0, direction.y), _speed * Time.deltaTime);
    }
}