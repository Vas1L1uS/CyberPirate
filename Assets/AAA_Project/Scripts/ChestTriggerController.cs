using System.Collections;
using UnityEngine;

public class ChestTriggerController : MonoBehaviour
{
    private bool _chestActive;

    [SerializeField] private GameObject _chestPanel;
    [SerializeField] private string _playerTag;
    [SerializeField] private float _timeToActivateChest;

    private void OnTriggerStay(Collider other)
    {
        if (_chestActive == false) return;

        if (other.CompareTag(_playerTag))
        {
            _chestActive = false;
            _chestPanel.SetActive(true);
        }
    }

    public void ActivateChest()
    {
        StartCoroutine(TimerToSchestActive(_timeToActivateChest));
    }

    private IEnumerator TimerToSchestActive(float time)
    {
        yield return new WaitForSeconds(time);
        _chestActive = true;
    }
}