using UnityEngine;

public class ChestTriggerController : MonoBehaviour
{
    public bool ChestActive;

    [SerializeField] private GameObject _chestPanel;
    [SerializeField] private string _playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (ChestActive) return;

        if (other.CompareTag(_playerTag))
        {
            ChestActive = true;
            _chestPanel.SetActive(true);
        }
    }
}