using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ArmorScriptableObject", order = 1)]
public class ArmorScriptableObject : ScriptableObject
{
    [SerializeField]
    private string armorName;
    public string ArmorName
    {
        get => armorName; 
    }
    [SerializeField]
    private float armorDamage;
    public float ArmorDamage
    {
        get => armorDamage; 
    }
    [SerializeField]
    private int weaponAttackRange;
    public int WeaponAttackRange
    {
        get => weaponAttackRange; 
    }
    [SerializeField]
    private int weaponAttackCoolTime;
    public int WeaponAttackCoolTime
    {
        get => weaponAttackCoolTime; 
    }
}
