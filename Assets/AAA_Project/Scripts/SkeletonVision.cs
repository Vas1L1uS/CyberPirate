using System;
using UnityEngine;

public class SkeletonVision : MonoBehaviour
{
    public event EventHandler PlayerInZone_notifier;
    public event EventHandler PlayerExitZone_notifier;

    [SerializeField] private string _playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            PlayerInZone_notifier?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            PlayerExitZone_notifier?.Invoke(this, EventArgs.Empty);
        }
    }
}