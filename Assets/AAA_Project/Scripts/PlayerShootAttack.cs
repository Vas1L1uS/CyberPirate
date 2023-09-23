using System;
using UnityEngine;

public class PlayerShootAttack : MonoBehaviour
{
    public event EventHandler Shoot_notifier;
    public event EventHandler<IntValueEventArgs> AmmoChanged_notifier;

    public int BulletLeft { get => _bulletLeft; set => _bulletLeft = value; }
    public int Damage => _damage;
    public int MaxBullets => _maxBullets;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private GameObject _bullet_prefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private AudioSource _audioSource;

    [Header("Debug")]
    [SerializeField] private int _bulletLeft;

    private int _damage;
    private int _maxBullets;

    private void Start()
    {
        _playerController.Input.Player.Shoot.performed += Context => Shoot();
    }

    public void SetMaxAmmo()
    {
        _bulletLeft = _maxBullets;
        AmmoChanged_notifier?.Invoke(this, new IntValueEventArgs() { Value = _bulletLeft });
    }

    public void SetNewMaxAmmo(int count)
    {
        _maxBullets = count;
        SetMaxAmmo();
    }

    public void SetNewDamage(int count)
    {
        _damage = count;
    }

    public void Shoot()
    {
        if (_bulletLeft <= 0)
        {
            return;
        }
        _audioSource.Play();
        Vector3 direction = (new Vector3(_playerController.MouseWorldOnGroundPos.x, _playerController.MouseWorldOnGroundPos.y + 1.5f, _playerController.MouseWorldOnGroundPos.z)
            - _bulletSpawnPoint.position).normalized;

        GameObject newBullet = Instantiate(_bullet_prefab, _bulletSpawnPoint.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetNewDamage(Damage);
        newBullet.GetComponent<Rigidbody>().velocity = direction * _bulletSpeed;
        _bulletLeft--;
        Shoot_notifier?.Invoke(this, EventArgs.Empty);
        AmmoChanged_notifier?.Invoke(this, new IntValueEventArgs() { Value = _bulletLeft });
    }
}