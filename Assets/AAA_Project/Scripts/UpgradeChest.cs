using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeChest : MonoBehaviour
{
    public event Action ItemUpgraded;

    public PlayerController _playerController;
    public int MaxMeleeUpgrade => _maxMeleeUpgrade;
    public int MaxRangeUpgrade => _maxRangeUpgrade;
    public int MaxHealthUpgrade => _maxHealthUpgrade;
    public int MaxAmmoUpgrade => _maxAmmoUpgrade;

    public int CurrentMeleeUpgrade { get; private set; } = 0;
    public int CurrentRangeUpgrade { get; private set; } = 0;
    public int CurrentHealthUpgrade { get; private set; } = 0;
    public int CurrentAmmoUpgrade { get; private set; } = 0;


    [SerializeField] private UpdateConfig _updateConfig;

    [Header("MaxUpgradeSettings")]
    [SerializeField] private int _maxMeleeUpgrade;
    [SerializeField] private int _maxRangeUpgrade;
    [SerializeField] private int _maxHealthUpgrade;
    [SerializeField] private int _maxAmmoUpgrade;

    [Header("Weapon")]
    [SerializeField] private MeshRenderer _weaponMeshRenderer;
    [SerializeField] private List<Material> _weaponMaterials;

    [Header("Bullet")]
    [SerializeField] private List<Material> _bulletMaterials;

    private int _currentWeaponMaterialIndex = 0;
    private int _currentBulletMaterialIndex = 0;

    public bool UpgradeMelee()
    {
        if (CurrentMeleeUpgrade == MaxMeleeUpgrade) return false;

        _playerController.MeleeAttack.SetNewDamage(_playerController.MeleeAttack.Damage + _updateConfig.UpdateMeleeDamage);
        _currentWeaponMaterialIndex++;
        CurrentMeleeUpgrade++;
        _weaponMeshRenderer.material = _weaponMaterials[_currentWeaponMaterialIndex];

        ItemUpgraded?.Invoke();
        return true;
    }

    public bool UpgradeRange()
    {
        if (CurrentRangeUpgrade == MaxRangeUpgrade) return false;

        _playerController.ShootAttack.SetNewDamage(_playerController.ShootAttack.Damage + _updateConfig.UpdateShootDamage);
        _currentBulletMaterialIndex++;
        CurrentRangeUpgrade++;
        _playerController.ShootAttack.BulletMaterial = _bulletMaterials[_currentBulletMaterialIndex];

        ItemUpgraded?.Invoke();
        return true;
    }

    public bool UpgradeHealth()
    {
        if (CurrentHealthUpgrade == MaxHealthUpgrade) return false;

        _playerController.Health.SetNewMaxHealth(_playerController.Health.MaxHealth + _updateConfig.UpdateHealth);
        CurrentHealthUpgrade++;

        ItemUpgraded?.Invoke();
        return true;
    }

    public bool UpgradeAmmo()
    {
        if (CurrentAmmoUpgrade == MaxAmmoUpgrade) return false;

        _playerController.ShootAttack.SetNewMaxAmmo(_playerController.ShootAttack.MaxBullets + _updateConfig.UpdateMaxAmmo);
        CurrentAmmoUpgrade++;

        ItemUpgraded?.Invoke();
        return true;
    }
}