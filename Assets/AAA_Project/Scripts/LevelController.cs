using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LevelConfig _level;
    [SerializeField] private GameObject _skeleton_prefab;
    [SerializeField] private GameObject _hardSkeleton_prefab;
    [SerializeField] private List<Transform> _skeletonSpawnerPoints;
    [SerializeField] private AudioSource _levelAudioSource;
    [SerializeField] private ChestController _chestController;

    private List<SkeletonController> _skeletons = new List<SkeletonController>();
    private LevelSettings _levelSettings;
    private PlayerController _playerController;

    private bool _isSpawning;

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

        _chestController.UpgradeChest.ItemUpgraded += StartLevel;
        StartLevel();
    }

    private void DeadSkeleton(object sender, EventArgs e)
    {
        var skeletonHealth = sender as CharacterHealth;
        var skeleton = skeletonHealth.GetComponent<SkeletonController>();
        _skeletons.Remove(skeleton);

        if (_skeletons.Count == 0 && _isSpawning == false)
        {
            FinishLevel();
        }
    }

    private void FinishLevel()
    {
        _levelSettings.SkeletonHealth += _level.AddedSkeletonHealth_NextLevel;
        _levelSettings.SkeletonDamage += _level.AddedSkeletonDamage_NextLevel;
        _levelSettings.SkeletonCount += _level.AddedCountSkeleton_NextLevel;

        _chestController.TriggerController.ActivateChest();
        _chestController.AnimController.ChestUp();
    }

    private void StartLevel()
    {
        _chestController.AnimController.ChestDown();
        _levelAudioSource.Play();
        _playerController.Health.SetMaxHealth();
        _playerController.ShootAttack.SetMaxAmmo();

        StartCoroutine(TimerSpawn());
    }

    private IEnumerator TimerSpawn()
    {
        _isSpawning = true;
        for (int i = 0; i < _levelSettings.SkeletonCount; i++)
        {
            var randomPoint = UnityEngine.Random.Range(0, _skeletonSpawnerPoints.Count);
            GameObject skeleton;

            if (i % 6 == 0 && i != 0)
            {
                skeleton = Instantiate(_hardSkeleton_prefab, _skeletonSpawnerPoints[randomPoint].position, Quaternion.identity);
            }
            else
            {
                skeleton = Instantiate(_skeleton_prefab, _skeletonSpawnerPoints[randomPoint].position, Quaternion.identity);
            }

            var skeletonHealth = skeleton.GetComponent<CharacterHealth>();
            skeletonHealth.Dead_notifier += DeadSkeleton;
            var skeletonC = skeleton.GetComponent<SkeletonController>();

            skeletonC.Player = _player;
            skeletonC.Damage = _levelSettings.SkeletonDamage;

            if (skeletonC.TypeSkeleton == TypeSkeleton.hardS)
            {
                skeletonC.Health = (int)Math.Round(_levelSettings.SkeletonHealth * 1.5f);
            }
            else
            {
                skeletonC.Health = _levelSettings.SkeletonHealth ;
            }

            _skeletons.Add(skeletonC);

            yield return new WaitForSeconds(1);
        }

        _isSpawning = false;
    }

    private class LevelSettings
    {
        public int SkeletonHealth;
        public int SkeletonDamage;
        public int SkeletonCount;
    }
}