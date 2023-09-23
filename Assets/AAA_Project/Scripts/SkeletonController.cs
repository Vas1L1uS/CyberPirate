using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    public Transform Player;
    public int Damage { get;set; }
    public int Health { get; set; }

    [SerializeField] private SkeletonAttack _skeletonAttack;
    [SerializeField] private CharacterHealth _characterHealth;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Collider _collider;

    private bool _isStopped = false;

    private bool _isActiveScript = true;

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
        if (_isActiveScript)
        {
            _agent.destination = Player.position;

            if (_isStopped) return;
            this.transform.LookAt(new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z));
        }
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _isStopped = true;
    }

    public void Go()
    {
        _agent.isStopped = false;
        StartCoroutine(Timer());
    }

    private void Die(object sender, EventArgs e)
    {
        _isActiveScript = false;
        StopAllCoroutines();
        Stop();
        Destroy(_collider);
        Destroy(this);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.2f);
        _isStopped = false;
    }
}