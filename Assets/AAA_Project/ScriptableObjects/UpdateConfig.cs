using UnityEngine;

[CreateAssetMenu(fileName = "UpdateConf", menuName = "ScriptableObjects/UpdateConfig", order = 1)]
public class UpdateConfig : ScriptableObject
{
    public int UpdateMeleeDamage;
    public int UpdateShootDamage;
    public int UpdateHealth;
    public int UpdateMaxAmmo;
}