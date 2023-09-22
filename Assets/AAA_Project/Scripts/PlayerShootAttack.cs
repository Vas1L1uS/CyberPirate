using System;
using UnityEngine;

public class PlayerShootAttack : MonoBehaviour
{
    public event EventHandler Shoot_notifier;

    public int BulletLeft => _bulletLeft;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private GameObject _bullet_prefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private AudioSource _audioSource;

    [Header("Debug")]
    [SerializeField] private int _bulletLeft;

    private void Start()
    {
        _playerController.Input.Player.Shoot.performed += Context => Shoot();
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
        newBullet.GetComponent<Rigidbody>().velocity = direction * _bulletSpeed;
        _bulletLeft--;

        Shoot_notifier?.Invoke(this, EventArgs.Empty);
    }
}