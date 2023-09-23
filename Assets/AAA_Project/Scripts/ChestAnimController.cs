using UnityEngine;

public class ChestAnimController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ChestUp()
    {
        _animator.SetTrigger("Up");
    }

    public void ChestDown()
    {
        _animator.SetTrigger("Down");
    }
}
