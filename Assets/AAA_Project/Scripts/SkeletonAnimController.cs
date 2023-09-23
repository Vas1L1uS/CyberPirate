using System;
using System.Collections;
using UnityEngine;

public class SkeletonAnimController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SkeletonAttack _skeletonAttack;
    [SerializeField] private CharacterHealth _skeletonHealth;

    private void Start()
    {
        _skeletonAttack.Attack_notifier += Attack;
        _skeletonHealth.Dead_notifier += Die;
    }

    private void Attack(object sender, EventArgs e)
    {
        _animator.SetTrigger("Attack");
    }

    private void Die(object sender, EventArgs e)
    {
        _animator.Play("Dead");
        StartCoroutine(TimerToDestroy());
    }

    private IEnumerator TimerToDestroy()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    }
}