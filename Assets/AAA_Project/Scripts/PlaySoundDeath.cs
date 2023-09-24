using System;
using UnityEngine;

public class PlaySoundDeath : MonoBehaviour
{
    [SerializeField] private CharacterHealth _playerHealth;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _deathAudioSource;

    private void Awake()
    {
        _playerHealth.Dead_notifier += PlayDead;
    }

    private void PlayDead(object sender, EventArgs e)
    {
        _audioSource.Stop();
        _deathAudioSource.Play();
    }
}