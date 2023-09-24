using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    public bool MusicIsActive { get; private set; } = true;

    [SerializeField] private LevelController _levelController;
    [SerializeField] private CharacterHealth _playerHealth;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _pausePanel;

    private PlayerInput _playerInput;
    private bool _isPaused;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Pause.performed += context => PauseKey();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void PauseKey()
    {
        if (_levelController.StopGame) return;

        if (_playerHealth.CurrentHealth == 0) return;

        if (_isPaused)
        {
            Resume();
        }
        else
        {
            _pausePanel.SetActive(true);
            _gamePanel.SetActive(false);
            _isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        _pausePanel.SetActive(false);
        _gamePanel.SetActive(true);
        _isPaused = false;
        Time.timeScale = 1;
    }

    public void Music()
    {
        if (MusicIsActive)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.Play();
        }

        MusicIsActive = !MusicIsActive;
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
