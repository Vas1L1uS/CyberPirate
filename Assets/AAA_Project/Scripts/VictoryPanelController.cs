using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanelController : MonoBehaviour
{
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private ChestController _chestController;

    public void Continue()
    {
        _pauseController.IsPaused = false;
        Time.timeScale = 1;
        _chestController.TriggerController.ActivateChest();
        _chestController.AnimController.ChestUp();
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}