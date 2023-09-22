using System;
using UnityEngine;

public class PlayerMoveAnimController: MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerRotation _playerRotation;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _idleParameter;
    [SerializeField] private string _changeParameter;
    [SerializeField] private string _upParameter;
    [SerializeField] private string _upRightParameter;
    [SerializeField] private string _rightParameter;
    [SerializeField] private string _downRightParameter;
    [SerializeField] private string _downParameter;
    [SerializeField] private string _downLeftParameter;
    [SerializeField] private string _leftParameter;
    [SerializeField] private string _upLeftParameter;

    private PlayerInput _playerInput;
    private AnimState _animState;

    private void Start()
    {
        _playerInput = _playerController.Input;
    }


    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector2 inputDirection = _playerInput.Player.Movement.ReadValue<Vector2>();

        AnimState animState = AnimState.Idle;

        if (inputDirection == Vector2.zero) animState = AnimState.Idle;
        else
        {
            var angle = Vector2.SignedAngle(new Vector2((int)Math.Round(inputDirection.x), (int)Math.Round(inputDirection.y)), new Vector2((int)Math.Round(_playerRotation.Direction.x), (int)Math.Round(_playerRotation.Direction.y)));
            Debug.Log($"{_playerRotation.Direction}, {inputDirection}     {angle}");

            if (angle == 0) animState = AnimState.Up;
            else if (angle == 45) animState = AnimState.UpRight;
            else if (angle == 90) animState = AnimState.Right;
            else if (angle == 135) animState = AnimState.DownRight;
            else if (angle == 180) animState = AnimState.Down;
            else if (angle == -135) animState = AnimState.DownLeft;
            else if (angle == -90) animState = AnimState.Left;
            else if (angle == -45) animState = AnimState.UpLeft;
        }

        if (animState != _animState)
        {
            _animState = animState;

            switch (animState)
            {
                case AnimState.Idle:
                    _animator.SetTrigger(_idleParameter);
                    break;
                case AnimState.Up:
                    _animator.SetTrigger(_upParameter);
                    break;
                case AnimState.UpRight:
                    _animator.SetTrigger(_upRightParameter);
                    break;
                case AnimState.Right:
                    _animator.SetTrigger(_rightParameter);
                    break;
                case AnimState.DownRight:
                    _animator.SetTrigger(_downRightParameter);
                    break;
                case AnimState.Down:
                    _animator.SetTrigger(_downParameter);
                    break;
                case AnimState.DownLeft:
                    _animator.SetTrigger(_downLeftParameter);
                    break;
                case AnimState.Left:
                    _animator.SetTrigger(_leftParameter);
                    break;
                case AnimState.UpLeft:
                    _animator.SetTrigger(_upLeftParameter);
                    break;
            }
        }
    }

    private enum AnimState
    {
        Idle,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft
    }
}