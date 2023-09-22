using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IDamager
{
    public event EventHandler GaveDamage_notifier;
    public int Damage => _damage;

    [SerializeField] private string _senderTag;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private ParticleSystem _boomParticleSystem;
    [SerializeField] private int _damage;
    [SerializeField] private float _timerToDestroy;
    [SerializeField] private GameObject _render;
    [SerializeField] private LayerMask _collisionMask;

    private Collider _myCollider;
    private Rigidbody _myRB;


    private void Awake()
    {
        _myCollider = this.GetComponent<Collider>();
        _myRB = this.GetComponent<Rigidbody>();
    }

    public void GiveDamage(IHealth target)
    {
        GaveDamage_notifier?.Invoke(this, EventArgs.Empty);
        target.GetDamage(Damage);
    }

    public void SetNewDamage(int damage)
    {
        if (damage > 0) _damage = damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if (StandartMethods.CheckBitInNumber(collision.gameObject.layer, _collisionMask) == false)
        //{
        //    return;
        //}

        if (collision.CompareTag(_senderTag))
        {
            return;
        }


        Instantiate(_boomParticleSystem, this.transform.position, Quaternion.identity);

        if (collision.TryGetComponent<IHealth>(out var target))
        {
            target.GetDamage(Damage);
        }

        StartCoroutine(TimerToDestroy(_timerToDestroy));
    }

    private IEnumerator TimerToDestroy(float timerToDestroy)
    {
        Destroy(_render);
        _myCollider.enabled = false;
        _myRB.velocity = Vector3.zero;
        _myRB.isKinematic = true;
        yield return new WaitForSeconds(timerToDestroy);
        Destroy(this.gameObject);
    }
}