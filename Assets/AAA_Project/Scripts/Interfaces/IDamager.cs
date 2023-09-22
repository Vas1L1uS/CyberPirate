using System;

public interface IDamager
{
    event EventHandler GaveDamage_notifier;

    int Damage { get; }
    void GiveDamage(IHealth target);
    void SetNewDamage(int damage);
}