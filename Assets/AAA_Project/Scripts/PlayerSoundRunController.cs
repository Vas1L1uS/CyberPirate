using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundRunController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _grassAudioClips;
    [SerializeField] private List<AudioClip> _sandAudioClips;
    [SerializeField] private LayerMask _grassLayerMask;
    [SerializeField] private LayerMask _sandLayerMask;
    [SerializeField] private float _time;

    private Coroutine _coroutine;
    private int _currentClipIndex;
    private Vector2 _inputDirection;
    private SurfaceType _currentSurface;


    private void Update()
    {
        if (_playerController.Health.CurrentHealth == 0) return;

        _inputDirection = _playerController.Input.Player.Movement.ReadValue<Vector2>();

        if (_inputDirection != Vector2.zero)
        {
            if (Physics.Raycast(this.transform.position, Vector3.down, 0.5f, _grassLayerMask))
            {
                if (_currentSurface == SurfaceType.grass) return;
                _currentSurface = SurfaceType.grass;
                if (_coroutine != null) StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(PlayGrassSound(_time));
            }
            else if (Physics.Raycast(this.transform.position, Vector3.down, 0.5f, _sandLayerMask))
            {
                if (_currentSurface == SurfaceType.sand) return;
                _currentSurface = SurfaceType.sand;
                if (_coroutine != null) StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(PlaySandSound(_time));
            }
        }
        else
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            _currentSurface = SurfaceType.none;
        }
    }

    private IEnumerator PlayGrassSound(float time)
    {
        while (_inputDirection != Vector2.zero)
        {
            SetRandomAudionIndex(_grassAudioClips.Count);
            _audioSource.clip = _grassAudioClips[_currentClipIndex];
            _audioSource.Play();
            yield return new WaitForSeconds(time);
        }
    }

    private IEnumerator PlaySandSound(float time)
    {
        while (_inputDirection != Vector2.zero)
        {
            SetRandomAudionIndex(_sandAudioClips.Count);
            _audioSource.clip = _sandAudioClips[_currentClipIndex];
            _audioSource.Play();
            yield return new WaitForSeconds(time);
        }
    }

    private void SetRandomAudionIndex(int count)
    {
        int audioIndex = int.MaxValue;

        while (audioIndex == _currentClipIndex || audioIndex == int.MaxValue)
        {
            audioIndex = Random.Range(0, count);
        }
        //Debug.Log(audioIndex);
        _currentClipIndex = audioIndex;
    }

    private enum SurfaceType
    {
        none,
        grass,
        sand
    }
}