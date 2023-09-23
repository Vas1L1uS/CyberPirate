using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private CharacterHealth _playerHealth;
    [SerializeField] private PlayerShootAttack _playerShootAttack;
    [SerializeField] private Text _health;
    [SerializeField] private Text _ammo;

    private void Awake()
    {
        _playerHealth.HealthChanged_notifier += ChangeHealth;
        _playerShootAttack.AmmoChanged_notifier += ChangeAmmo;
    }

    private void ChangeHealth(object sender, IntValueEventArgs e)
    {
        _health.text = e.Value.ToString();
    }

    private void ChangeAmmo(object sender, IntValueEventArgs e)
    {
        _ammo.text = e.Value.ToString();
    }
}
