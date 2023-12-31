using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundTakeDamageController : MonoBehaviour
{
    [SerializeField] private CharacterHealth _characterHealth;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private AudioClip _deathClip;

    private int _currentClipIndex;

    private void Start()
    {
        _characterHealth.GetDamage_notifier += PlayAudio;
        _characterHealth.Dead_notifier += PlayDeathAudio;
    }

    private void SetRandomAudionIndex(int count)
    {
        int audioIndex = int.MaxValue;

        while (audioIndex == _currentClipIndex || audioIndex == int.MaxValue)
        {
            audioIndex = UnityEngine.Random.Range(0, count);
        }
        _currentClipIndex = audioIndex;
    }

    private void PlayAudio(object sender, EventArgs e)
    {
        SetRandomAudionIndex(_audioClips.Count);
        _audioSource.clip = _audioClips[_currentClipIndex];
        _audioSource.Play();
    }

    private void PlayDeathAudio(object sender, EventArgs e)
    {
        _audioSource.clip = _deathClip;
        _audioSource.Play();
    }

    private enum SurfaceType
    {
        none,
        grass,
        sand
    }
}