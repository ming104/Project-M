using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    private string weaponName;
    public string WeaponName
    {
        get => weaponName; 
    }
    [SerializeField]
    private float weaponDamage;
    public float WeaponDamage
    {
        get => weaponDamage; 
    }
    [SerializeField]
    private float weaponAttackRange;
    public float WeaponAttackRange
    {
        get => weaponAttackRange; 
    }
    [SerializeField]
    private float weaponAttackCoolTime;
    public float WeaponAttackCoolTime
    {
        get => weaponAttackCoolTime; 
    }
}
