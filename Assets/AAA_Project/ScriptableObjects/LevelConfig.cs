using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelConfigs/Level", order = 1)]
public class LevelConfig : ScriptableObject
{
    public int StartSkeletonHealth;
    public int StartSkeletonDamage;
    public int StartPlayerShootDamage;
    public int StartPlayerMeleeDamage;
    public int StartPlayerMaxAmmo;
    public int StartCountSkeletons;

    public int AddedCountSkeleton_NextLevel;
    public int AddedSkeletonHealth_NextLevel;
    public int AddedSkeletonDamage_NextLevel;

    public int AddedPoints;
}