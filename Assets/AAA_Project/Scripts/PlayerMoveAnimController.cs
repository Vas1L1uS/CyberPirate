using System;
using System.Collections;
using UnityEngine;

public class PlayerMoveAnimController: MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerAttackController _playerAttackController;
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
    [SerializeField] private string _isAttackedParameter;
    [SerializeField] private string _attackParameter;

    private PlayerInput _playerInput;
    private AnimState _animState;

    private bool _isReadyToAttack = true;

    private void Start()
    {
        _playerInput = _playerController.Input;
        _playerAttackController.Attack_notifier += Attack;
        _playerAttackController.ReadyAttack_notifier += ReadyToAttack;
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
            //Debug.Log($"{_playerRotation.Direction}, {inputDirection}     {angle}");

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
            SetAllMoveAnimFalse();

            switch (animState)
            {
                case AnimState.Idle:
                    _animator.SetBool(_idleParameter, true);
                    break;
                case AnimState.Up:
                    _animator.SetBool(_upParameter, true);
                    break;
                case AnimState.UpRight:
                    _animator.SetBool(_upRightParameter, true);
                    break;
                case AnimState.Right:
                    _animator.SetBool(_rightParameter, true);
                    break;
                case AnimState.DownRight:
                    _animator.SetBool(_downRightParameter, true);
                    break;
                case AnimState.Down:
                    _animator.SetBool(_downParameter, true);
                    break;
                case AnimState.DownLeft:
                    _animator.SetBool(_downLeftParameter, true);
                    break;
                case AnimState.Left:
                    _animator.SetBool(_leftParameter, true);
                    break;
                case AnimState.UpLeft:
                    _animator.SetBool(_upLeftParameter, true);
                    break;
            }
        }
    }

    private void Attack(object sender, EventArgs e)
    {
        _animator.SetTrigger(_attackParameter);
        _animator.SetBool(_isAttackedParameter, true);
    }

    private void ReadyToAttack(object sender, EventArgs e)
    {
        _animator.SetBool(_isAttackedParameter, false);
    }

    private void SetAllMoveAnimFalse()
    {
        _animator.SetBool(_idleParameter, false);
        _animator.SetBool(_upParameter, false);
        _animator.SetBool(_upRightParameter, false);
        _animator.SetBool(_rightParameter, false);
        _animator.SetBool(_downRightParameter, false);
        _animator.SetBool(_downParameter, false);
        _animator.SetBool(_downLeftParameter, false);
        _animator.SetBool(_leftParameter, false);
        _animator.SetBool(_upLeftParameter, false);
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