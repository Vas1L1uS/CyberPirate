using UnityEngine;

[CreateAssetMenu(fileName = "UpdateConf", menuName = "ScriptableObjects/UpdateConfig", order = 1)]
public class UpdateConfig : ScriptableObject
{
    public int UpdateWeapontDamage;
    public int UpdateShootDamage;
    public int UpdateHealth;
}