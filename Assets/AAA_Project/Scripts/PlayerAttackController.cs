using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public event EventHandler Attack_notifier;
    public event EventHandler ReadyAttack_notifier;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _reloadTime;

    private PlayerInput _playerInput;
    private bool _isReadyToAttack = true;

    private void Start()
    {
        _playerInput = _playerController.Input;

        _playerInput.Player.Attack.performed += context => Attack();
    }

    private void Attack()
    {
        if (_isReadyToAttack)
        {
            _audioSource.Play();
            StartCoroutine(ReloadAttack());
        }
    }

    private IEnumerator ReloadAttack()
    {
        _isReadyToAttack = false;
        Attack_notifier?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(_reloadTime);
        ReadyAttack_notifier?.Invoke(this, EventArgs.Empty);
        _isReadyToAttack = true;
    }
}