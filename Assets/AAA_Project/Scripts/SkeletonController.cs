using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    public Transform Player { get => _player; set => _player = value; }

    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _agent.destination = _player.position;
        _agent.isStopped = false;
    }

    private void Update()
    {
        _agent.destination = _player.position;
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _animator.SetTrigger("Attack");
    }

    public void Go()
    {
        _agent.isStopped = false;
    }
}