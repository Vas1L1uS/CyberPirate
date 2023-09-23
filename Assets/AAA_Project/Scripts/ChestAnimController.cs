using UnityEngine;

public class ChestAnimController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ChestUp()
    {
        _animator.Play("Up");
    }

    public void ChestDown()
    {
        _animator.Play("Down");
    }
}
