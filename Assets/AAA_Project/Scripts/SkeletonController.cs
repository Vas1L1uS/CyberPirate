using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    public Transform Player { get => _player; set => _player = value; }

    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    private bool _isStopped = false;

    private void Start()
    {
        _agent.destination = _player.position;
        _agent.isStopped = false;
    }

    private void Update()
    {
        _agent.destination = _player.position;

        if (_isStopped) return;
        this.transform.LookAt(new Vector3(_player.transform.position.x, this.transform.position.y, _player.transform.position.z));
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _isStopped = true;
        _animator.SetTrigger("Attack");
    }

    public void Go()
    {
        _agent.isStopped = false;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.2f);
        _isStopped = false;
    }
}