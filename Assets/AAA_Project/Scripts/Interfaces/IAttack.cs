using System;

internal interface IAttack
{
    public event EventHandler Attack_notifier;
    public event EventHandler ReadyAttack_notifier;

    bool IsReadyToAttack { get; }

    void Attack();
}