using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LevelConfig _level;
    [SerializeField] private GameObject _skeleton_prefab;
    [SerializeField] private List<Transform> _skeletonSpawnerPoints;
    [SerializeField] private AudioSource _levelAudioSource;

    private List<SkeletonController> _skeletons = new List<SkeletonController>();
    private LevelSettings _levelSettings;
    private PlayerController _playerController;

    private void Awake()
    {

        _levelSettings = new LevelSettings()
        {
            SkeletonCount = _level.StartCountSkeletons,
            SkeletonDamage = _level.StartSkeletonDamage,
            SkeletonHealth = _level.StartSkeletonHealth
        };
    }

    private void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _playerController.Health.SetNewMaxHealth(_level.StartPlayerMaxHealth);
        _playerController.ShootAttack.SetNewMaxAmmo(_level.StartPlayerMaxAmmo);
        _playerController.ShootAttack.SetNewDamage(_level.StartPlayerShootDamage);
        _playerController.MeleeAttack.SetNewDamage(_level.StartPlayerMeleeDamage);

        StartLevel(_levelSettings);
    }

    private void DeadSkeleton(object sender, EventArgs e)
    {
        var skeletonHealth = sender as CharacterHealth;
        var skeleton = skeletonHealth.GetComponent<SkeletonController>();
        _skeletons.Remove(skeleton);

        if (_skeletons.Count == 0)
        {
            FinishLevel();
        }
    }

    private void SpawnSkeletons(int countSkeletons)
    {
        for (int i = 0; i < countSkeletons; i++)
        {
            var randomPoint = UnityEngine.Random.Range(0, _skeletonSpawnerPoints.Count);
            var skeleton = Instantiate(_skeleton_prefab, _skeletonSpawnerPoints[randomPoint].position, Quaternion.identity);
            var skeletonHealth = skeleton.GetComponent<CharacterHealth>();
            skeletonHealth.Dead_notifier += DeadSkeleton;
            var skeletonC = skeleton.GetComponent<SkeletonController>();
            _skeletons.Add(skeletonC);
        }
    }

    private void FinishLevel()
    {
        _levelSettings.SkeletonHealth += _level.AddedSkeletonHealth_NextLevel;
        _levelSettings.SkeletonDamage += _level.AddedSkeletonDamage_NextLevel;
        _levelSettings.SkeletonCount += _level.AddedCountSkeleton_NextLevel;

        StartLevel(_levelSettings);
    }

    private void StartLevel(LevelSettings levelSettings)
    {
        _levelAudioSource.Play();
        _playerController.Health.SetMaxHealth();
        _playerController.ShootAttack.SetMaxAmmo();

        SpawnSkeletons(levelSettings.SkeletonCount);

        foreach (var skeleton in _skeletons)
        {
            skeleton.Player = _player;
            skeleton.Damage = levelSettings.SkeletonDamage;
            skeleton.Health = levelSettings.SkeletonHealth;
        }
    }

    private class LevelSettings
    {
        public int SkeletonHealth;
        public int SkeletonDamage;
        public int SkeletonCount;
    }
}