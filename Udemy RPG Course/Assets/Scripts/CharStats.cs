using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [SerializeField] string charName = "";
    [SerializeField] int playerLevel = 1;
    [SerializeField] int maxLevel = 100;
    [SerializeField] int baseEXP = 1000;
    [SerializeField] int maxHP = 500;
    [SerializeField] int maxMP = 100;
    [SerializeField] int strength = 1;
    [SerializeField] int defence = 1;
    [SerializeField] int weaponPower = 10;
    [SerializeField] int armorPower = 10;
    [SerializeField] string equippedWeapon = "";
    [SerializeField] string equippedArmor = "";
    [SerializeField] Sprite charImage = null;

    public string CharName { get => charName; }
    public int PlayerLevel { get => playerLevel; }
    public int MaxLevel { get => maxLevel; }
    public int BaseEXP { get => baseEXP; }
    public int MaxHP { get => maxHP; }
    public int MaxMP { get => maxMP; }
    public int Strength { get => strength; }
    public int Defence { get => defence; }
    public int WeaponPower { get => weaponPower; set => weaponPower = value; }
    public int ArmorPower { get => armorPower; set => armorPower = value; }
    public string EquippedWeapon { get => equippedWeapon; set => equippedWeapon = value; }
    public string EquippedArmor { get => equippedArmor; set => equippedArmor = value; }
    public Sprite CharImage { get => charImage; set => charImage = value; }

    public int[] expToNextLevel { get; private set; }
    public int currentEXP { get; private set; }
    public int currentHP { get; private set; }
    public int currentMP { get; private set; }
    
    private void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;
        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    public void AddEXP(int expToAdd)
    {
        if (playerLevel < maxLevel)
        {
            currentEXP += expToAdd;
            if (currentEXP >= expToNextLevel[playerLevel])
            {
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;
                if (playerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }
                maxHP += 50 * playerLevel;
                currentHP = maxHP;
                maxMP += 10 * playerLevel;
                currentMP = maxMP;
            }
        }
        if (playerLevel == maxLevel)
        {
            currentEXP = 0;
        }
    }

    public void AddHP(int HPToAdd)
    {
        currentHP += HPToAdd;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void AddMP(int MPToAdd)
    {
        currentMP += MPToAdd;
        if (currentMP > maxMP)
        {
            currentMP = maxMP;
        }
    }

    public void AddStr(int StrToAdd)
    {
        strength += StrToAdd;
    }
}
