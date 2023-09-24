using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [SerializeField] private float _time;

    private void Start()
    {
        StartCoroutine(TimerToLoadScene(_time));
    }

    private IEnumerator TimerToLoadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
