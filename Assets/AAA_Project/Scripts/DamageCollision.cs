using System;
using UnityEngine;

public class DamageCollision : MonoBehaviour, IDamager
{
    public event EventHandler GaveDamage_notifier;

    public int Damage => _damage;

    [SerializeField] private LayerMask _collisionLayerMask;
    [SerializeField] private int _damage;
    [SerializeField] private bool _destroyBeforeCollision;

    public void SetNewDamage(int damage)
    {
        _damage = damage;
    }

    public void GiveDamage(IHealth target)
    {
        target.GetDamage(_damage);
        GaveDamage_notifier?.Invoke(this, EventArgs.Empty);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (StandartMethods.CheckBitInNumber(collision.gameObject.layer, _collisionLayerMask))
        {
            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                GiveDamage(health);
            }
        }
        else return;
    }
}
