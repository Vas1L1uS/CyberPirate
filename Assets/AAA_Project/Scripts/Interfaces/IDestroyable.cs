using System;

internal interface IDestroyable
{
    event EventHandler Destroy_notifier;
}
