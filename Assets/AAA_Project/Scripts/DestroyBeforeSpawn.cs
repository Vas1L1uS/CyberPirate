using System.Collections;
using UnityEngine;

public class DestroyBeforeSpawn : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;

    private void Start()
    {
        StartCoroutine(TimerToDestroy(_timeToDestroy));
    }

    private IEnumerator TimerToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
