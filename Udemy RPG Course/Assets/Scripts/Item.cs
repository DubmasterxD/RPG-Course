using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    [SerializeField] bool isItem = false;
    [SerializeField] bool isWeapon = false;
    [SerializeField] bool isArmour = false;
    [Header("Item Details")]
    [SerializeField] string itemName = "";
    [SerializeField] string description = "";
    [SerializeField] int value = 0;
    [SerializeField] Sprite itemSprite = null;
    [SerializeField] int amountToChange = 0;
    [SerializeField] bool affectHP = false, affectMP = false, affectStr = false;
    [Header("Weapon/Armor Details")]
    [SerializeField] int weaponStrength = 0;
    [SerializeField] int armorStrength = 0;

    public string ItemName { get => itemName; }
    public Sprite ItemSprite { get => itemSprite; }
    public bool IsItem { get => isItem; }
    public bool IsWeapon { get => isWeapon; }
    public bool IsArmour { get => isArmour; }
    public string Description { get => description; }
    public int Value { get => value; }

    public void Use(int charToUseOn)
    {
        CharStats selectedChar = GameManager.instance.PlayerStats[charToUseOn];
        if (isItem)
        {
            if (affectHP)
            {
                selectedChar.AddHP(amountToChange);
            }
            if (affectMP)
            {
                selectedChar.AddMP(amountToChange);
            }
            if (affectStr)
            {
                selectedChar.AddStr(amountToChange);
            }
        }
        if (isWeapon)
        {
            if (selectedChar.EquippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedChar.EquippedWeapon);
            }
            selectedChar.EquippedWeapon = itemName;
            selectedChar.WeaponPower = weaponStrength;
        }
        if (isArmour)
        {
            if (selectedChar.EquippedArmor != "")
            {
                GameManager.instance.AddItem(selectedChar.EquippedArmor);
            }
            selectedChar.EquippedArmor = itemName;
            selectedChar.ArmorPower = armorStrength;
        }
        GameManager.instance.RemoveItem(itemName);
    }
}