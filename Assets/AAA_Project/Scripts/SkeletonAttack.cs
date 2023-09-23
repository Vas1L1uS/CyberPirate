using System;
using System.Collections;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour, IDamager, IAttack
{
    public event EventHandler GaveDamage_notifier;
    public event EventHandler Attack_notifier;
    public event EventHandler ReadyAttack_notifier;

    public int Damage => _damage;
    public bool IsReadyToAttack => _isReadyToAttack;

    [SerializeField] private SkeletonController _skeletonController;
    [SerializeField] private CharacterHealth _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _distanceAttack;
    [SerializeField] private float _timeToDamage;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _damage;
    [SerializeField] private bool _isReadyToAttack = true;

    private void Update()
    {
        if (Vector2.Distance(new Vector2(_player.transform.position.x, _player.transform.position.z), new Vector2(this.transform.position.x, this.transform.position.z)) < _distanceAttack)
        {
            Attack();
        }
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

    public void Attack()
    {
        if (_isReadyToAttack)
        {
            _skeletonController.Stop();
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
        _skeletonController.Go();
    }
}