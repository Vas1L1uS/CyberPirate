using UnityEngine;
using UnityEngine.AI;

public class TestSkeleton : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        _agent.destination = _player.position;
        _agent.isStopped = false;
    }

    private void FixedUpdate()
    {
        _agent.destination = _player.position;
    }
}