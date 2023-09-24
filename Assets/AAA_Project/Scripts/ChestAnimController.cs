using UnityEngine;

public class ChestAnimController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clipUp;
    [SerializeField] private AudioClip _clipDown;

    public void ChestUp()
    {
        _animator.Play("Up");
        _audioSource.clip = _clipUp;
        _audioSource.Play();
    }

    public void ChestDown()
    {
        _animator.Play("Down");
        _audioSource.clip = _clipDown;
        _audioSource.Play();
    }
}
