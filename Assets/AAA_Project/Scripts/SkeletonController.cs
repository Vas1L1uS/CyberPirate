using System;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    public Transform Player;
    public int Damage { get;set; }
    public int Health { get; set; }
    public TypeSkeleton TypeSkeleton;


    [SerializeField] private SkeletonAttack _skeletonAttack;
    [SerializeField] private CharacterHealth _characterHealth;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Collider _collider;

    private bool _isStopped = false;

    private void Start()
    {
        _skeletonAttack.SetNewDamage(Damage);
        _characterHealth.SetNewMaxHealth(Health);

        _agent.destination = Player.position;
        _agent.isStopped = false;
        _characterHealth.Dead_notifier += Die;
    }

    private void Update()
    {
        if (_agent != null) _agent.destination = Player.position;

        if (_isStopped) return;
        this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
    }

    public void Stop()
    {
        if (_agent != null) _agent.isStopped = true;
        _isStopped = true;
    }

    public void Go()
    {
        if (_agent != null) _agent.isStopped = false;
        _isStopped = false;
    }

    private void Die(object sender, EventArgs e)
    {
        Stop();
        Destroy(_collider);
        Destroy(_agent);
        Destroy(this);
    }
}

public enum TypeSkeleton
{
    defaultS,
    hardS
}