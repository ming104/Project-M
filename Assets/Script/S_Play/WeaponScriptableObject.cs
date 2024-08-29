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
    
    [Header("additional"), SerializeField] 
    private int additionalHp;
    public int AdditionalHp
    {
        get => additionalHp;
    }
    [SerializeField] 
    private int additionalMp;
    public int AdditionalMp
    {
        get => additionalMp;
    }
    [SerializeField] 
    private int additionalDef;
    public int AdditionalDef
    {
        get => additionalDef;
    }
    [SerializeField] 
    private int additionalPower;
    public int AdditionalPower
    {
        get => additionalPower;
    }
    [SerializeField] 
    private int additionalIntelligence;
    public int AdditionalIntelligence
    {
        get => additionalIntelligence;
    }
    [SerializeField] 
    private int additionalJustice;
    public int AdditionalJustice
    {
        get => additionalJustice;
    }
    [SerializeField] 
    private int additionalMovementSpeed;
    public int AdditionalMovementSpeed
    {
        get => additionalMovementSpeed;
    }
}
