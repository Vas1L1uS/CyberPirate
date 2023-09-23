using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour, IAttack, IDamager
{
    public event EventHandler Attack_notifier;
    public event EventHandler ReadyAttack_notifier;
    public event EventHandler GaveDamage_notifier;

    public bool IsReadyToAttack => _isReadyToAttack;

    public int Damage => _damage;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SphereZone _sphereZone;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeToDamage;
    [SerializeField] private float _reloadTime;

    private PlayerInput _playerInput;
    private bool _isReadyToAttack = true;

    private void Start()
    {
        _playerInput = _playerController.Input;

        _playerInput.Player.Attack.performed += context => Attack();
    }

    public void Attack()
    {
        if (_playerController.Health.CurrentHealth == 0) return;

        if (_isReadyToAttack)
        {
            _audioSource.Play();
            StartCoroutine(ReloadAttack());
        }
    }

    private void AllDamage()
    {
        if (_sphereZone.Hits.Count > 0)
        {
            foreach (var item in _sphereZone.Hits)
            {
                if (item.transform.gameObject.TryGetComponent(out IHealth health)) GiveDamage(health);
            }
        }
    }

    private IEnumerator ReloadAttack()
    {
        _isReadyToAttack = false;
        Attack_notifier?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(_timeToDamage);
        AllDamage();
        yield return new WaitForSeconds(_reloadTime - _timeToDamage);
        ReadyAttack_notifier?.Invoke(this, EventArgs.Empty);
        _isReadyToAttack = true;
    }

    public void GiveDamage(IHealth target)
    {
        GaveDamage_notifier?.Invoke(this, EventArgs.Empty);
        target.GetDamage(Damage);
    }

    public void SetNewDamage(int damage)
    {
        if (damage > 0) _damage = damage;
    }
}