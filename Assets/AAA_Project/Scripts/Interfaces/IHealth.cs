using System;

public interface IHealth
{
    event EventHandler Dead_notifier;
    event EventHandler<IntValueEventArgs> HealthChanged_notifier;
    event EventHandler<IntValueEventArgs> GetDamage_notifier;
    event EventHandler<IntValueEventArgs> GetHealth_notifier;
    event EventHandler<IntValueEventArgs> NewMaxHealth_notifier;
    bool IsAlive { get; }
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void GetDamage(int damage);
    void GetHealth(int health);
    void SetMaxHealth();
    void SetNewMaxHealth(int maxHealth);
    void Die();
}