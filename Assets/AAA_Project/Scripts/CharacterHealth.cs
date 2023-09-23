using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IHealth, IEditorDebugLogger
{
    public bool IsAlive => _isAlive;
    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    public bool EnabledPrintDebugLogInEditor { get => _enabledPrintDebugLogInEditor; set => _enabledPrintDebugLogInEditor = value; }

    public event EventHandler Dead_notifier;
    public event EventHandler<IntValueEventArgs> GetDamage_notifier;
    public event EventHandler<IntValueEventArgs> GetHealth_notifier;
    public event EventHandler<IntValueEventArgs> NewMaxHealth_notifier;

    [SerializeField] private int _maxHealth;

    [Header("Debug")]
    [SerializeField] private bool _enabledPrintDebugLogInEditor;
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isAlive;

    private void Awake()
    {
        SetMaxHealth();
    }

    public void Die()
    {
        _isAlive = false;
        Dead_notifier?.Invoke(this, EventArgs.Empty);
        PrintLogInEditor($"{this.gameObject.name} killed.");
    }

    public void GetDamage(int damage)
    {
        if (damage > 0 && _currentHealth > 0)
        {
            var previousHealthPoints = CurrentHealth;
            _currentHealth -= damage;
            GetDamage_notifier?.Invoke(this, new IntValueEventArgs() { Value = damage });

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }

            PrintLogInEditor($"{this.gameObject.name} received {damage} damage. Was {previousHealthPoints} health, current {CurrentHealth}, max {MaxHealth}. Removed {previousHealthPoints - CurrentHealth} HP.");
        }
        else
        {
            PrintLogInEditor($"Damage < 0. {this.gameObject.name} is not damaged.");
        }

    }

    public void GetHealth(int health)
    {
        if (health > 0)
        {
            var previousHealthPoints = CurrentHealth;
            _currentHealth += health;
            GetHealth_notifier?.Invoke(this, new IntValueEventArgs() { Value = health });

            PrintLogInEditor($"{this.gameObject.name} received {health} health. Was {previousHealthPoints} health, current {CurrentHealth}, max {MaxHealth}. Added {CurrentHealth - previousHealthPoints} HP.");
        }
    }

    public void SetMaxHealth()
    {
        _currentHealth = MaxHealth;
    }

    public void SetNewMaxHealth(int maxHealth)
    {
        if (maxHealth > 0)
        {
            _maxHealth = maxHealth;
            SetMaxHealth();
            NewMaxHealth_notifier?.Invoke(this, new IntValueEventArgs() { Value = maxHealth });
        }
    }

    public void PrintLogInEditor(string text)
    {
        if (EnabledPrintDebugLogInEditor)
        {
            Debug.Log(text);
        }
    }
}
