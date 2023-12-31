using System;
using UnityEngine;

public class PlayerShootAttack : MonoBehaviour
{
    public event EventHandler Shoot_notifier;
    public event EventHandler<IntValueEventArgs> AmmoChanged_notifier;

    public Material BulletMaterial;

    public int BulletLeft { get => _bulletLeft; set => _bulletLeft = value; }
    public int Damage => _damage;
    public int MaxBullets => _maxBullets;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private GameObject _bullet_prefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _noAmmoAudioSource;

    [Header("Debug")]
    [SerializeField] private int _bulletLeft;

    [SerializeField] private int _damage;
    [SerializeField] private int _maxBullets;

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
        if (_playerController.Health.CurrentHealth == 0 || _playerController.PauseController.IsPaused) return;

        if (_bulletLeft <= 0)
        {
            _noAmmoAudioSource.Play();
            return;
        }
        _audioSource.Play();
        Vector3 direction = (new Vector3(_playerController.MouseWorldOnGroundPos.x, _playerController.MouseWorldOnGroundPos.y + 1.5f, _playerController.MouseWorldOnGroundPos.z)
            - _bulletSpawnPoint.position).normalized;


        GameObject newBullet = Instantiate(_bullet_prefab, _bulletSpawnPoint.position, Quaternion.identity);

        var bullet = newBullet.GetComponent<Bullet>();
        bullet.SetNewDamage(Damage);
        bullet.Material = BulletMaterial;

        newBullet.GetComponent<Rigidbody>().velocity = direction * _bulletSpeed;
        _bulletLeft--;
        Shoot_notifier?.Invoke(this, EventArgs.Empty);
        AmmoChanged_notifier?.Invoke(this, new IntValueEventArgs() { Value = _bulletLeft });
    }
}