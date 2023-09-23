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
    [SerializeField] private CharacterHealth _skeletonHealth;
    [SerializeField] private SkeletonVision _skeletonVision;
    [SerializeField] private SphereZone _sphereZone;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _timeToDamage;
    [SerializeField] private int _damage;
    [SerializeField] private bool _isReadyToAttack = true;

    private bool _playerNearby;

    private void Start()
    {
        _skeletonVision.PlayerInZone_notifier += PlayerNearbyTrue;
        _skeletonVision.PlayerExitZone_notifier += PlayerNearbyFalse;
        _skeletonHealth.Dead_notifier += Die;
    }

    private void Update()
    {
        if (_playerNearby)
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

    private void PlayerNearbyTrue(object sender, EventArgs e) { _playerNearby = true; }
    private void PlayerNearbyFalse(object sender, EventArgs e) { _playerNearby = false; }

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

    private void Die(object sender, EventArgs e)
    {
        StopAllCoroutines();
        Destroy(this);
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
        _skeletonController.Go();
    }
}