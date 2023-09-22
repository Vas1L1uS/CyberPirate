using System;

internal interface IAttack
{
    event EventHandler StartedAttack_notifier;
    event EventHandler FinishedAttack_notifier;

    bool IsReadyToAttack { get; }

    void StartAttack();
    void FinishAttack();
}